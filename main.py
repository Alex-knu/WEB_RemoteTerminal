from flask import Flask, request, jsonify
import Remote.RemoteLogic as rem
from DataBase import WorkWithDB as wwdb
import datetime
import Security.SingIn as auth
import Security.Hesh as hesh

app = Flask(__name__)


def my403(text):
    respons = jsonify({'error': text})
    respons.status_code = 403
    return respons


@app.route('/')
def hello():
    return 'Hello, World!'


@app.route('/connect', methods=['POST'])
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

    result = wwdb.GetUser(username)
    db_username = result.login
    db_password = result.password

    auth_status, auth_message = auth.authorization(username, password, db_username, db_password)

    if auth_status:

        if (use_ssh_key is False or use_ssh_key is None) and password is not None: 
            result = rem.execute_remote_command_pass(hostname, port, username, password, command, root_password)
        else:
            if rem.first_connect(hostname):
                if password is None:
                    return my403('It is impossible to establish SSH connect via keys without password for the first time')

                rem.keygen(hostname, username, password)

            result = rem.execute_remote_command_key(hostname, username, command, root_password)

        wwdb.SaveHistory('96799c6d-2bcc-4826-b8ef-50f1d502b662', command, datetime.datetime.now())
    else:
        return auth_message

    # Но по нормальному должно было быть так
    # data = wwdb.GetUserMachine(request_data['machinName'], request_data['user'], request_data['password'])
    # if data is None or len(data) == 0:
    #     return 'Error: Not found user machin'
    #
    # wwdb.SaveHistory(data.machineGUID, command, datetime.datetime.now())
    # result = rem.execute_remote_command(data.host, data.port, data.username, data.password, data.command)

    return result


@app.route('/gethistory', methods=['GET'])
def GetHistory():
    request_data = request.get_json()

    if 'userGUID' not in request_data:
        return my403('There is no userGUID in the request')

    userGUID = request_data['userGUID']

    result = wwdb.GetHistory(userGUID)

    return str(result)


@app.route('/savemachine', methods=['POST'])
def SaveMachine():
    request_data = request.get_json()

    if 'userGUID' not in request_data:
        return my403('There is no userGUID in the request')
    if 'host' not in request_data:
        return my403('There is no host in the request')
    if 'port' not in request_data:
        return my403('There is no port in the request')
    if 'username' not in request_data:
        return my403('There is no username in the request')
    if 'command' not in request_data:
        return my403('There is no command in the request')

    userGUID = request_data['userGUID']
    host = request_data['host']
    port = request_data['port']
    username = request_data['username']
    password = request_data['password']

    wwdb.SaveMachine(userGUID, host, port, username, password)


@app.route('/saveuser', methods=['POST'])
def SaveUser():
    request_data = request.get_json()

    if 'login' not in request_data:
        return my403('There is no login in the request')
    if 'name' not in request_data:
        return my403('There is no name in the request')
    if 'password' not in request_data:
        return my403('There is no password in the request')

    login = request_data['login']
    name = request_data['name']
    password = hesh.heshing(request_data['password'])

    wwdb.SaveUser(login, name, password)


@app.route('/updateuser', methods=['POST'])
def UpdateUser():
    request_data = request.get_json()

    if 'userGUID' not in request_data:
        return my403('There is no userGUID in the request')
    if 'login' not in request_data and 'name' not in request_data and 'password' not in request_data:
        return my403('The query has no parameters to change(login, name, password)')

    guid = request_data['userGUID']
    login = request_data['login'] if 'login' in request_data else None
    name = request_data['name'] if 'name' in request_data else None
    password = hesh.heshing(request_data['password']) if 'password' in request_data else None

    wwdb.UpdateUser(guid, login, name, password)


@app.route('/updatemachine', methods=['POST'])
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


@app.route('/deleteuser', methods=['POST'])
def DeleteUser():
    request_data = request.get_json()

    if 'userGUID' not in request_data:
        return my403('There is no userGUID in the request')

    guid = request_data['userGUID']

    wwdb.DeleteUser(guid)


@app.route('/deletemachine', methods=['POST'])
def DeleteMachine():
    request_data = request.get_json()

    if 'machineGUID' not in request_data:
        return my403('There is no machineGUID in the request')

    guid = request_data['machineGUID']

    wwdb.DeleteMachine(guid)


if __name__ == '__main__':
    app.run()
