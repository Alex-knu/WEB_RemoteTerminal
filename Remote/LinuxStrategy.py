import subprocess

def execute_linux_local(command: str) -> subprocess.CompletedProcess:
    """
    Executes given Linux command on the current (local) machine

    :param str command: command to be executed on the current machine
    :return: result of command execution
    :rtype: subprocess.CompletedProcess
    """
    return subprocess.run(command, shell=True, stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)

def create_dir(hostname: str):
    """
    Creates directory on the current (local) machine according to the format: "./keys/<hostname>"

    :param str hostname: hostname of the remote host
    """
    execute_linux_local(f'mkdir -p keys/{hostname}')

def keygen_linux(hostname: str, username: str):
    """
    Generates SSH key pair and prepares it to the usage

    :param str hostname: hostname of the remote host
    :param str username: username of the remote host
    """
    create_dir(hostname)
    execute_linux_local(f"ssh-keygen -b 2048 -t rsa -f keys/{hostname}/id_rsa -q -N \"\"")
    execute_linux_local(f"chmod 600 keys/{hostname}/*")
    add_ssh_key(hostname)
    add_trusted_host(hostname, username)

def add_ssh_key(hostname: str):
    """
    Adds private key to the OpenSSH authentication agent

    :param str hostname: hostname of the remote host
    """
    execute_linux_local(f"eval `ssh-agent`")
    execute_linux_local(f"ssh-add -q keys/{hostname}/id_rsa")

def add_trusted_host(hostname: str, username: str):
    """
    Adds remote host to the local list of trusted hosts

    :param str hostname: hostname of the remote host
    :param str username: username of the remote host
    """
    execute_linux_local(f"ssh-keyscan -H {username},{hostname} >> ~/.ssh/known_hosts")

def ssh_copy_id(hostname: str, username: str, password: str):
    """
    Copies public SSH key to the remote host, particularly to the ~/.ssh/authorized_keys

    :param str hostname: hostname of the remote host
    :param str username: username of the remote host
    :param str username: password of the user of the remote host
    """
    execute_linux_local(f"sshpass -p {password} ssh-copy-id -i keys/{hostname}/id_rsa.pub {username}@{hostname}")

def check_if_dir_exist(hostname: str) -> bool:
    """
    Checks if directory exists

    :param str hostname: hostname of the remote host
    :return: True if directory exists and False otherwise
    :rtype: bool
    """
    if execute_linux_local(f'[ -d "keys/{hostname}" ] && echo "true"').returncode == 0:
        return True
    else:
        return False
