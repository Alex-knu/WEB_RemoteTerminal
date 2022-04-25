from flask_login import UserMixin

class Users(UserMixin):
    def __init__(self, guid, login, password, name):
        self.guid = guid
        self.login = login
        self.password = password
        self.name = name