from flask import Flask, request
import Remote.RemoteLogic as rem
from WEB_RemoteTerminal.DataBase import WorkWithDB as wwdb

app = Flask(__name__)


@app.route('/')
def hello():
    return 'Hello, World!'


@app.route('/connect', methods=['POST'])
def Connect():
    request_data = request.get_json()

    host = request_data['host']
    port = request_data['port']
    username = request_data['username']
    password = request_data['password']
    command = request_data['command']
    root_password = request_data['rootPassword'] if 'rootPassword' in request_data else None

    result = rem.execute_remote_command(host, port, username, password, command, root_password)

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

    guid = request_data['guid']
    login = request_data['login']
    name = request_data['name']
    password = request_data['password']

    wwdb.UpdateUser(guid, login, name, password)


@app.route('/updatevachine', methods=['POST'])
def UpdateMachine():
    request_data = request.get_json()

    guid = request_data['guid']
    host = request_data['host']
    port = request_data['port']
    password = request_data['password']

    wwdb.UpdateMachine(guid, host, port, password)


if __name__ == '__main__':
    app.run()
