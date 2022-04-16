import subprocess

def execute_linux_local(command):
    return subprocess.run(command, shell=True)

def keygen_linux(hostname):
    return execute_linux_local(f"ssh-keygen -b 2048 -t rsa -f keys/{hostname}/id_rsa -q -N \"\"")

def check_if_dir_exist(hostname):
    if execute_linux_local(f'[ -d "keys/{hostname}" ] && echo "true"').returncode == 0:
        return True
    else:
        return False

def create_dir_linux(hostname):
    return execute_linux_local(f'mkdir -p keys/{hostname}')
