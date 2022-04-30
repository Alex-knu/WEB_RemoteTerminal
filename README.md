This is *Remote Terminal* web app designed to execute bash commands on remote Ubuntu hosts. The main architecture is:

<img src="arch.jpg" width="350" />

### Launch instructions

App is compatible both with Windows and Linux devices. Before running the program perform the following:

- make sure you have `python3` preinstalled

- install all necessary requirements by running:

    ```
    pip install -r requirements.txt
    ```

- Run <a href="Files/DB_Postgres.sql">DB_Postgres.sql</a> script on your database instance

- Write your database credentials to the <a href="Files/settings.ini">settings.ini</a> file 

On Linux:

- install `sshpass` utility:

  ```
  sudo apt-get -y install sshpass
  ```

On Windows:

- make sure that *ssh-agent* service is running by executing:
  
  ```
    Get-Service ssh-agent | Set-Service -StartupType Manual
    Start-Service ssh-agent
    Get-Service ssh-agent
  ```
  
Finally, you just have to run `main.py` file.
