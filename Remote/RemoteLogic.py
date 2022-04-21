import paramiko
import Remote.WindowsStrategy as ws
import Remote.LinuxStrategy as ls
import platform
import time
import re

def execute_command_ssh(session, command, root_password):
    channel = session.get_transport().open_session()
    channel.get_pty()
    channel.exec_command(command)

    if root_password is not None:
        channel.send(root_password + '\n')

    time.sleep(1)
    lines = channel.recv(1024)
    channel.close()
    lines = str(lines)

    if root_password is not None:
        answ = re.search(":.*", lines).group(0)[2:-1].replace("\\r", "")
    else:
        answ = lines[2:-1].replace("\\r", "")

    answ = re.sub(' +', ' ', answ)
    answ = answ.replace("\\n", "\n")
    answ = answ.replace("\\t", "\t")

    return answ

def execute_remote_command_pass(host: str, port: int, username: str, password: str, command: str, root_password: str):
    """
    Connects to the Remote host and executes given command

    :param str sudoPassword: root password
    :param str host: hostname or IP address of the Remote host
    :param int port: SSH port number
    :param str username: name of the user of the Remote host
    :param str password: username password
    :param str command: command will be executed on the Remote host
    :return: result of command execution
    :rtype: str
    """

    ssh = paramiko.SSHClient()
    ssh.set_missing_host_key_policy(paramiko.AutoAddPolicy())
    ssh.connect(host, port, username, password)

    answ = execute_command_ssh(session=ssh, command=command, root_password=root_password)

    ssh.close()

    return answ

def execute_remote_command_key(hostname, username, command, root_password):
    session = paramiko.SSHClient()
    session.set_missing_host_key_policy(paramiko.AutoAddPolicy())
    key_file = paramiko.RSAKey.from_private_key_file(f"keys/{hostname}/id_rsa")
    session.connect(hostname=hostname,username=username, pkey=key_file)

    answ = execute_command_ssh(session=session, command=command, root_password=root_password)

    session.close()

    return answ

def check_os():
    if platform.system() == "Windows":
        return "Windows"
    elif platform.system() == "Linux":
        return "Linux"
    else:
        raise Exception('Unknown operating system!')


def first_connect(hostname: str) -> bool:
    if check_os() == "Windows":
        pass
    elif check_os() == "Linux":
        if ls.check_if_dir_exist(hostname):
            return False
        return True
    else:
        raise Exception('Unknown operating system!')

def keygen(hostname: str, username: str, password: str):
    if check_os() == "Windows":
        pass
    elif check_os() == "Linux":
        ls.keygen_linux(hostname, username)
        ls.ssh_copy_id(hostname, username, password)
    else:
        raise Exception('Unknown operating system!')

#if first_connect(hostname):
#    keygen(hostname, username, password)
#print(execute_remote_command_key(hostname, username,command,None))
