from flask import Flask, request
import Remote.RemoteLogic as rem
from DataBase import WorkWithDB as wwdb
import datetime

app = Flask(__name__)


@app.route('/')
def hello():
    return 'Hello, World!'


@app.route('/connect', methods=['POST'])
def Connect():
    request_data = request.get_json()

    use_ssh_key = request_data["UseSSHKey"] if 'UseSSHKey' in request_data else None
    hostname = request_data['host']
    port = request_data['port']
    username = request_data['username']
    password = request_data['password'] if 'password' in request_data else None
    command = request_data['command']
    root_password = request_data['rootPassword'] if 'rootPassword' in request_data else None

    if (use_ssh_key is False or use_ssh_key is None) and password is not None:
        result = rem.execute_remote_command_pass(hostname, port, username, password, command, root_password)
    else:
        if rem.first_connect(hostname) and password is None:
            raise Exception("It is impossible to establish SSH connect via keys without password for the first time")
        else:
            if rem.first_connect(hostname):
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
def GetHistory():
    request_data = request.get_json()

    userGUID = request_data['userGUID']

    result = wwdb.GetHistory(userGUID)

    return str(result)


@app.route('/savemachine', methods=['POST'])
def SaveMachine():
    request_data = request.get_json()

    userGUID = request_data['userGUID']
    host = request_data['host']
    port = request_data['port']
    username = request_data['username']
    password = request_data['password']

    wwdb.SaveMachine(userGUID, host, port, username, password)


@app.route('/saveuser', methods=['POST'])
def SaveUser():
    request_data = request.get_json()

    login = request_data['login']
    name = request_data['name']
    password = request_data['password']

    wwdb.SaveUser(login, name, password)


@app.route('/updateuser', methods=['POST'])
def UpdateUser():
    request_data = request.get_json()

    guid = request_data['userGUID']
    login = request_data['login'] if 'login' in request_data else None
    name = request_data['name'] if 'name' in request_data else None
    password = request_data['password'] if 'password' in request_data else None

    wwdb.UpdateUser(guid, login, name, password)


@app.route('/updatemachine', methods=['POST'])
def UpdateMachine():
    request_data = request.get_json()

    guid = request_data['machineGUID']
    host = request_data['host'] if 'host' in request_data else None
    port = request_data['port'] if 'port' in request_data else None
    password = request_data['password'] if 'password' in request_data else None

    wwdb.UpdateMachine(guid, host, port, password)


if __name__ == '__main__':
    app.run()
