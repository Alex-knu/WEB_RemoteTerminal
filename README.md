### Launch instructions

On Linux:

- install `sshpass` utility

On Windows:

- make sure that *ssh-agent* service is running by running:
  
  ```
    Get-Service ssh-agent | Set-Service -StartupType Manual
    Start-Service ssh-agent
    Get-Service ssh-agent
  ```
