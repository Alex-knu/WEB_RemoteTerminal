import subprocess

def execute_windows_local(command):
    return subprocess.run(["powershell", "-Command", command], capture_output=True)

def keygen_windows(hostname):
    return execute_windows_local(f"ssh-keygen -b 2048 -t rsa -f keys/{hostname}/id_rsa -q -N '\"\"'")

def create_dir_windows(hostname):
    return execute_windows_local(f'New-Item -Path "keys/" -Name "{hostname}" -ItemType "directory"')

#def sshcopyid_windows(hostname, username, password):
#    return subprocess.run(["powershell", "-Command", f"type keys/{hostname}/id_rsa.pub | ssh {username}@{hostname} 'cat >> .ssh/authorized_keys'"], capture_output=True)
