import subprocess

def execute_windows_local(command: str) -> subprocess.CompletedProcess:
    """
    Executes given Windows command on the current (local) machine

    :param str command: command to be executed on the current machine
    :return: result of command execution
    :rtype: subprocess.CompletedProcess
    """
    return subprocess.run(["powershell", "-Command", command], capture_output=True)

def keygen_windows(hostname: str):
    """
    Generates SSH key pair and prepares it to the usage

    :param str hostname: hostname of the remote host
    """
    create_dir(hostname)
    execute_windows_local(f"ssh-keygen -b 2048 -t rsa -f keys/{hostname}/id_rsa -q -N '\"\"'")
    add_ssh_key(hostname)

def add_ssh_key(hostname: str):
    """
    Adds private key to the OpenSSH authentication agent

    :param str hostname: hostname of the remote host
    """
    #Get-Service ssh-agent | Set-Service -StartupType Manual
    #Start-Service ssh-agent
    #Get-Service ssh-agent
    execute_windows_local(f"ssh-add -q .\Remote\keys\{hostname}\id_rsa")

def ssh_copy_id(hostname: str, username: str, password: str):
    """
    Copies public SSH key to the remote host, particularly to the ~/.ssh/authorized_keys

    :param str hostname: hostname of the remote host
    :param str username: username of the remote host
    :param str username: password of the user of the remote host
    """
    execute_windows_local(f".\pscp -pw {password} .\keys\{hostname}\id_rsa.pub {username}@{hostname}:/home/{username}/.ssh/authorized_keys")

def check_if_dir_exist(hostname: str) -> bool:
    """
    Checks if directory exists

    :param str hostname: hostname of the remote host
    :return: True if directory exists and False otherwise
    :rtype: bool
    """
    if "True" in str(execute_windows_local(f'Test-Path -Path keys/{hostname}').stdout):
        return True
    else:
        return False

def create_dir(hostname: str):
    """
    Creates directory on the current (local) machine according to the format: "./keys/<hostname>"

    :param str hostname: hostname of the remote host
    """
    execute_windows_local(f'New-Item -Path "keys/" -Name "{hostname}" -ItemType "directory"')
