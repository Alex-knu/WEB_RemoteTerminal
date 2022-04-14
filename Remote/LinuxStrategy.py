import subprocess

def execute_linux_local(command):
    return subprocess.run(command, shell=True)

def keygen_linux(hostname):
    return execute_windows_local(f"ssh-keygen -b 2048 -t rsa -f keys/{hostname}/id_rsa -q -N \"\"")

def create_dir_linux(hostname):
    return execute_linux_local(f'mkdir -p keys/{hostname}')
