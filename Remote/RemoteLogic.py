import paramiko
import WindowsStrategy
import LinuxStrategy

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
    return lines

    #example of usage
    #print(execute_remote_command(host="192.168.1.45", port=22, username="user1", password="password12345", command="hostnamectl"))

def check_os():
    if platform.system() == "Windows":
        return "Windows"
    elif platform.system() == "Linux":
        return "Linux"
    else:
        raise Exception('Unknown operating system!')

def keygen(hostname):
    if check_os() == "Windows":
        print(create_dir_windows(hostname))
        return keygen_windows(hostname)
    elif check_os() == "Linux":
        print(create_dir_linux(hostname))
        return keygen_linux(hostname)
    else:
        raise Exception('Unknown operating system!')
