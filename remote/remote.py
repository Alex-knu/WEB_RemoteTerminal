#pip install paramiko

import paramiko

def execute_command(host: str, port: int, username: str, password: str, command: str) -> str:
    """
    Connects to the remote host and executes given command

    :param str host: hostname or IP address of the remote host
    :param int port: SSH port number
    :param str username: name of the user of the remote host
    :param str password: username password
    :param str command: command will be executed on the remote host
    :return: result of command execution
    :rtype: str
    """

    ssh = paramiko.SSHClient()
    ssh.set_missing_host_key_policy(paramiko.AutoAddPolicy()) # add host to trusted host list
    ssh.connect(host, port, username, password)

    stdin, stdout, stderr = ssh.exec_command(command)
    lines = stdout.readlines()
    ssh.close()
    return lines

    #example of usage
    #print(execute_command(host="192.168.1.45", port=22, username="user1", password="password12345", command="hostnamectl"))
