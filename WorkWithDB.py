import mysql.connector
import configparser

config = configparser.ConfigParser()
config.read("Files/settings.ini")

mydb = mysql.connector.connect(
    host=config["DataBase"]["host"],
    user=config["DataBase"]["user"],
    password=config["DataBase"]["password"],
    database=config["DataBase"]["database"]
)

mycursor = mydb.cursor()


def GetHistory():
    result = mycursor.execute("SELECT * FROM HistoryToMachine")
    return [list(i) for i in result]


def GetUserMachine(machinName, user, password):
    # !!!Прилепить входящие данные функции в фильтрацию запроса!!!

    result = mycursor.execute("SELECT * FROM UserToMachine")
    return [list(i) for i in result]


def GetCommandToUser():
    result = mycursor.execute("SELECT * FROM CommandToUser")
    return [list(i) for i in result]
