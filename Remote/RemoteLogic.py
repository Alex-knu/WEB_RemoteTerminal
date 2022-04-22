import paramiko
import Remote.WindowsStrategy as ws
import Remote.LinuxStrategy as ls
import platform
import time
import re


def execute_command_ssh(session: paramiko.client.SSHClient, command: str, root_password: str) -> str:
    """
    Executes command in the given SSH session and processes the result

    :param paramiko.client.SSHClient session: session that the command will be executed in
    :param str command: command to be executed
    :param str root_password: password of the root user of the remote host
    :return: result of the given command
    :rtype: str
    """
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


def execute_remote_command_pass(hostname: str, port: int, username: str, password: str, command: str,
                                root_password: str):
    """
    Creates SSH session via password authentication and executes command in it

    :param str hostname: hostname of the remote host
    :param int port: SSH port number
    :param str username: username of the remote host
    :param str password: password of the user of the remote host
    :param str command: command to be executed on the remote host
    :param str root_password: password of the root user of the remote host
    :return: result of command execution
    :rtype: str
    """
    ssh = paramiko.SSHClient()
    ssh.set_missing_host_key_policy(paramiko.AutoAddPolicy())
    ssh.connect(hostname, port, username, password)

    answ = execute_command_ssh(session=ssh, command=command, root_password=root_password)
    ssh.close()
    return answ


def execute_remote_command_key(hostname: str, username: str, command: str, root_password: str) -> str:
    """
    Creates SSH session via SSH keys authentication and executes command in it

    :param str hostname: hostname of the remote host
    :param str username: username of the remote host
    :param str command: command to be executed on the remote host
    :param str root_password: password of the root user of the remote host
    :return: result of command execution
    :rtype: str
    """
    session = paramiko.SSHClient()
    session.set_missing_host_key_policy(paramiko.AutoAddPolicy())
    key_file = paramiko.RSAKey.from_private_key_file(f"keys/{hostname}/id_rsa")
    session.connect(hostname=hostname, username=username, pkey=key_file)

    answ = execute_command_ssh(session=session, command=command, root_password=root_password)

    session.close()

    return answ


def check_os() -> str:
    """
    Checks the OS type of the local machine

    :return: OS type of the host
    :rtype: str
    """
    if platform.system() == "Windows":
        return "Windows"
    elif platform.system() == "Linux":
        return "Linux"
    else:
        raise Exception('Unknown operating system!')


def first_connect(hostname: str) -> bool:
    """
    Checks if it's first connect to the remote host

    :param str hostname: hostname of the remote host
    :return: True if it's first connect and False otherwise
    :rtype: bool
    """
    if check_os() == "Windows":
        return not (ws.check_if_dir_exist(hostname))
    elif check_os() == "Linux":
        return not (ls.check_if_dir_exist(hostname))
    else:
        raise Exception('Unknown operating system!')


def keygen(hostname: str, username: str, password: str):
    """
    Generates SSH key pair and prepares it to the usage depending on the local OS type

    :param str hostname: hostname of the remote host
    :param str username: username of the remote host
    :param str password: password of the user of the remote host
    """
    if check_os() == "Windows":
        ws.keygen_windows(hostname)
        ws.ssh_copy_id(hostname, username, password)
    elif check_os() == "Linux":
        ls.keygen_linux(hostname, username)
        ls.ssh_copy_id(hostname, username, password)
    else:
        raise Exception('Unknown operating system!')
