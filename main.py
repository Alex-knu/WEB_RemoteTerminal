from flask import Flask, request
import Remote.RemoteLogic as rem
import WorkWithDB as wwdb

app = Flask(__name__)

def pretty_output(response):
    answ = ""
    for elem in response:
        answ += elem
    return answ


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

    result = rem.execute_remote_command(host, port, username, password, command)

    #Но по нормальному должно юыло быть так
    # data = wwdb.GetUserMachine(request_data['machinName'], request_data['user'], request_data['password'])
    # if data is None or len(data) == 0:
    #     return 'Error: Not found user machin'
    #
    # result = rem.execute_remote_command(data.host, data.port, data.username, data.password, data.command)

    return pretty_output(result)

#
if __name__ == '__main__':
    app.run()

# See PyCharm help at https://www.jetbrains.com/help/pycharm/
