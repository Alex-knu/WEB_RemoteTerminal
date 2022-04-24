import json

def authorization(username, password, db_usernames, db_passwords):
    status = False 
    if username in db_usernames:
        if password is not None:
            if password in db_passwords:
                status = True
                messages = "Verified"
            else:
                messages = "Access denied. Wrong password"
        else:
            messages = "Password can't be empty."
    else:
        messages = "Access denied. User not found."
    return status, messages
