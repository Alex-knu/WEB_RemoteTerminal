from flask import Flask, request, jsonify, session
import Remote.RemoteLogic as rem
from DataBase import WorkWithDB as wwdb
import datetime
import Security.SingIn as auth
import Security.Hesh as hesh
from flask_login import LoginManager
from flask_login import login_required, login_user, logout_user
import configparser

app = Flask(__name__)
login_manager = LoginManager()
login_manager.init_app(app)

config = configparser.ConfigParser()
config.read("Files/settings.ini")


app.config['SESSION_TYPE'] = config['AppConfig']['SESSION_TYPE']
app.config['SECRET_KEY'] = config['AppConfig']['SECRET_KEY']


@login_manager.user_loader
def load_user(login):
    return wwdb.GetUser(login)

def my403(text):
    respons = jsonify({'error': text})
    respons.status_code = 403
    return respons
def my401(text):
    respons = jsonify({'error': text})
    respons.status_code = 401
    return respons


@app.route('/')
def hello():
    return 'Hello, World!'


@app.route('/login', methods=['POST'])
def Auth():
    request_data = request.authorization
    if request_data.type is not 'basic':
        return my401('There is not basic auth type.')
    if request_data.username is None:
        return my403('There is no username in the request.')
    if request_data.password is None:
        return my403('There is no password in the request.')

    login = request_data.username
    password = hesh.heshing(request_data.password)
    user = wwdb.GetUser(login)
    session[user.login] = user.login

    if user is None:
        return my401('User not found.')

    db_login = user.login
    db_password = user.password

    auth_status, auth_message = auth.authorization(login, password, db_login, db_password)
    if auth_status:
        login_user(user)
    return auth_message
    

@app.route('/getsession', methods=['POST'])
@login_required
def getsession():
    request_data = request.get_json()
    if 'username' not in request_data:
        return my403('There is no username in the request.')

    login = request_data['username']
    if login in session:
        return f"User {login} is loged in."
    else:
        return f"User {login} is not loged in for now."


@app.route("/logout", methods=['POST'])
@login_required
def logout():
    request_data = request.get_json()
    if 'username' not in request_data:
        return my403('There is no username in the request.')

    login = request_data['username']

    session.pop(login,None)
    return f'User {login} was loged out.'


@app.route('/connect', methods=['POST'])
@login_required
def Connect():
    request_data = request.get_json()

    if 'host' not in request_data:
        return my403('There is no host in the request')
    if 'port' not in request_data:
        return my403('There is no port in the request')
    if 'username' not in request_data:
        return my403('There is no username in the request')
    if 'command' not in request_data:
        return my403('There is no command in the request')

    use_ssh_key = request_data["UseSSHKey"] if 'UseSSHKey' in request_data else None
    hostname = request_data['host']
    port = request_data['port']
    username = request_data['username']
    password = hesh.heshing(request_data['password']) if 'password' in request_data else None
    command = request_data['command']
    root_password = request_data['rootPassword'] if 'rootPassword' in request_data else None

    if (use_ssh_key is False or use_ssh_key is None) and password is not None: 
        result = rem.execute_remote_command_pass(hostname, port, username, password, command, root_password)
    else:
        if rem.first_connect(hostname):
            if password is None:
                return my403('It is impossible to establish SSH connect via keys without password for the first time')
            rem.keygen(hostname, username, password)
        result = rem.execute_remote_command_key(hostname, username, command, root_password)
    wwdb.SaveHistory('96799c6d-2bcc-4826-b8ef-50f1d502b662', command, datetime.datetime.now())

    # Но по нормальному должно было быть так
    # data = wwdb.GetUserMachine(request_data['machinName'], request_data['user'], request_data['password'])
    # if data is None or len(data) == 0:
    #     return 'Error: Not found user machin'
    #
    # wwdb.SaveHistory(data.machineGUID, command, datetime.datetime.now())
    # result = rem.execute_remote_command(data.host, data.port, data.username, data.password, data.command)

    return result


@app.route('/gethistory', methods=['GET'])
@login_required
def GetHistory():
    request_data = request.get_json()

    if 'userGUID' not in request_data:
        return my403('There is no userGUID in the request')

    userGUID = request_data['userGUID']

    result = wwdb.GetHistory(userGUID)

    return result


@app.route('/savemachine', methods=['POST'])
@login_required
def SaveMachine():
    request_data = request.get_json()

    if 'userGUID' not in request_data:
        return my403('There is no userGUID in the request')
    if 'machinename' not in request_data:
        return my403('There is no machinename in the request')
    if 'host' not in request_data:
        return my403('There is no host in the request')
    if 'user' not in request_data:
        return my403('There is no user in the request')
    if 'password' not in request_data:
        return my403('There is no command in the request')
    if 'port' not in request_data:
        return my403('There is no port in the request')

    userGUID = request_data['userGUID']
    machinename = request_data['machinename']
    host = request_data['host']
    user = request_data['user']
    password = request_data['password']
    port = request_data['port']

    wwdb.SaveMachine(userGUID, machinename, host, user, password, port)
    return 'New machine successfully added!'


@app.route('/saveuser', methods=['POST'])
#@login_required
def SaveUser():
    request_data = request.get_json()

    if 'login' not in request_data:
        return my403('There is no login in the request')
    if 'password' not in request_data:
        return my403('There is no password in the request')
    if 'name' not in request_data:
        return my403('There is no name in the request')

    login = request_data['login']
    password = request_data['password']
    name = request_data['name']

    wwdb.SaveUser(login, password, name)
    return 'New user successfully added!'


@app.route('/updateuser', methods=['POST'])
@login_required
def UpdateUser():
    request_data = request.get_json()

    if 'userGUID' not in request_data:
        return my403('There is no userGUID in the request')
    if 'login' not in request_data and 'name' not in request_data and 'password' not in request_data:
        return my403('The query has no parameters to change(login, name, password)')

    guid = request_data['userGUID']
    login = request_data['login'] if 'login' in request_data else None
    password = request_data['password'] if 'password' in request_data else None
    name = request_data['name'] if 'name' in request_data else None

    wwdb.UpdateUser(guid, login, password, name)
    return 'User successfully updated!'


@app.route('/updatemachine', methods=['POST'])
@login_required
def UpdateMachine():
    request_data = request.get_json()

    if 'machineGUID' not in request_data:
        return my403('There is no machineGUID in the request')
    if 'host' not in request_data and 'port' not in request_data and 'password' not in request_data:
        return my403('The query has no parameters to change(host, port, password)')

    guid = request_data['machineGUID']
    host = request_data['host'] if 'host' in request_data else None
    port = request_data['port'] if 'port' in request_data else None
    password = request_data['password'] if 'password' in request_data else None

    wwdb.UpdateMachine(guid, host, port, password)
    return 'Machine successfully updated!'


@app.route('/deleteuser', methods=['POST'])
@login_required
def DeleteUser():
    request_data = request.get_json()

    if 'userGUID' not in request_data:
        return my403('There is no userGUID in the request')

    guid = request_data['userGUID']

    wwdb.DeleteUser(guid)
    return 'User successfully deleted!'


@app.route('/deletemachine', methods=['POST'])
@login_required
def DeleteMachine():
    request_data = request.get_json()

    if 'machineGUID' not in request_data:
        return my403('There is no machineGUID in the request')

    guid = request_data['machineGUID']

    wwdb.DeleteMachine(guid)
    return 'Machine successfully deleted!'


@app.errorhandler(401)
def unauthorized(error):
    return 'User not authorized. Please, login or register your account.'


if __name__ == '__main__':
    app.run()
