import mysql.connector
import configparser
import uuid

config = configparser.ConfigParser()
config.read("Files/settings.ini")


# !!!Откроем когда будет бд
# mydb = mysql.connector.connect(
#     host=config["DataBase"]["host"],
#     user=config["DataBase"]["user"],
#     password=config["DataBase"]["password"],
#     database=config["DataBase"]["database"]
# )
#
# mycursor = mydb.cursor()


def GetHistory(userGUID):
    # !!! join таблиц UserToMachine и HistoryToMachine по UserToMachine.GUID = HistoryToMachine.MachineGUID + where
    # UserToMachine.UserGUID = userGUID !!!

    # result = mycursor.execute("SELECT * FROM HistoryToMachine")
    # return [list(i) for i in result]
    return 1


def GetUserMachine(machinName, user, password):
    # !!!Прилепить входящие данные функции в фильтрацию запроса!!!

    # result = mycursor.execute("SELECT * FROM UserToMachine")
    # return [list(i) for i in result]
    return 1


def GetCommandToUser():
    # result = mycursor.execute("SELECT * FROM CommandToUser")
    # return [list(i) for i in result]
    return 1


def SaveMachine(userGUID, host, port, username, password):
    newGUID = uuid.uuid4()
    # ну и сейв в бд. Без ретурна


def SaveHistory(machineGUID, command, time):
    newGUID = uuid.uuid4()
    # ну и сейв в бд. Без ретурна


def SaveUser(login, password, name):
    newGUID = uuid.uuid4()
    # ну и сейв в бд. Без ретурна

def UpdateUser(guid, login, password, name):
    return 1

def UpdateMachine(guid, host, port, password):
    return 1

def DeleteUser(guid):
    return 1

def DeleteMachine(guid):
    return 1