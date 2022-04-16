import paramiko
import Remote.WindowsStrategy as ws
import Remote.LinuxStrategy as ls
import platform
import time
import re


def execute_remote_command(host: str, port: int, username: str, password: str, command: str) -> str:
    """
    Connects to the Remote host and executes given command

    :param str host: hostname or IP address of the Remote host
    :param int port: SSH port number
    :param str username: name of the user of the Remote host
    :param str password: username password
    :param str command: command will be executed on the Remote host
    :return: result of command execution
    :rtype: str
    """

    ssh = paramiko.SSHClient()
    ssh.set_missing_host_key_policy(paramiko.AutoAddPolicy()) # add host to trusted host list
    ssh.connect(host, port, username, password)

    stdin, stdout, stderr = ssh.exec_command(command)
    lines = stdout.readlines()
    ssh.close()
    answ = ""
    for elem in lines:
        answ += elem
    return answ

    #example of usage
    #print(execute_remote_command(host="192.168.1.45", port=22, username="user1", password="password12345", command="hostnamectl"))

def execute_remote_command_sudo(host: str, port: int, username: str, password: str, command: str, sudo_password: str):
    ssh = paramiko.SSHClient()
    ssh.set_missing_host_key_policy(paramiko.AutoAddPolicy())
    ssh.connect(host, port, username, password)
    channel = ssh.get_transport().open_session()
    channel.get_pty()
    #channel.settimeout(5)
    channel.exec_command(command)
    channel.send(sudo_password + '\n')
    time.sleep(1)
    lines=channel.recv(1024)
    channel.close()
    lines = str(lines)
    answ = re.search(":.*", lines).group(0)[2:-1].replace("\\r","")
    answ = re.sub(' +', ' ', answ)
    answ = answ.replace("\\n","\n")
    answ = answ.replace("\\t","\t")
    return answ

def check_os():
    if platform.system() == "Windows":
        return "Windows"
    elif platform.system() == "Linux":
        return "Linux"
    else:
        raise Exception('Unknown operating system!')

def keygen(hostname):
    if check_os() == "Windows":
        if not ws.check_if_dir_exist(hostname):
            ws.create_dir_windows(hostname)
            ws.keygen_windows(hostname)
            return ws.keygen_windows(hostname)
    elif check_os() == "Linux":
        if not ls.check_if_dir_exist(hostname):
            ls.create_dir_linux(hostname)
            ls.keygen_linux(hostname)
            return ls.keygen_linux(hostname)
    else:
        raise Exception('Unknown operating system!')
