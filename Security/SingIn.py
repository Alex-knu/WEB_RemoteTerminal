import json

def authorization(username, password, db_usernames, db_passwords):
    status = False 
    if username == db_usernames:
        if password:
            if password == db_passwords:
                status = True
                messages = "Authorized."
            else:
                messages = "Access denied. Wrong password."
        else:
            messages = "Password can't be empty."
    else:
        messages = "Access denied. User not found."
    return status, messages
