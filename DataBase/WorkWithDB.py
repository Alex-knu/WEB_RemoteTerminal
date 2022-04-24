import configparser
import psycopg2
import mysql.connector
import uuid
from .Models.Users import Users
from .Models.UserToMachine import UserToMachine
from .Models.HistoryToMachine import HistoryToMachine

config = configparser.ConfigParser()
config.read("Files/settings.ini")


def GetConnection():
    if config['DataBase']['type'] == 'PostgreSQL':
        connection = psycopg2.connect(
            host=config['DataBase']['host'],
            user=config['DataBase']['user'],
            password=config['DataBase']['password'],
            database=config['DataBase']['database']
        )
        connection.autocommit = True
    elif config['DataBase']['type'] == 'MySQL':
        connection = mysql.connector.connect(
            host=config["DataBase"]["host"],
            user=config["DataBase"]["user"],
            password=config["DataBase"]["password"],
            database=config["DataBase"]["database"]
        )

    return connection


def SaveUser(login, password, name):
    connection = GetConnection()
    newGUID = uuid.uuid4()
    with connection.cursor() as cursor:
        cursor.execute(f"""INSERT INTO "Users" ("Guid", "Login", "Password", "Name") 
        VALUES ('{newGUID}', '{login}', '{password}', '{name}')""")
    connection.close()


def SaveMachine(userGUID, machineName, host, username, password, port):
    connection = GetConnection()
    newGUID = uuid.uuid4()
    with connection.cursor() as cursor:
        cursor.execute(f"""INSERT INTO "UserToMachine" 
        ("Guid", "UserGUID", "MachineName", "Host", "User", "Password", "Port") VALUES 
        ('{newGUID}', '{userGUID}', '{machineName}', '{host}', '{username}', '{password}', '{port}')""")
    connection.close()


def SaveHistory(machineGUID, command, time):
    connection = GetConnection()
    newGUID = uuid.uuid4()
    with connection.cursor() as cursor:
        cursor.execute(f"""INSERT INTO "HistoryToMachine" ("Guid", "MachineGUID", "Command", "Time") 
        VALUES ('{newGUID}', '{machineGUID}', '{command}', '{time}')""")
    connection.close()


def UpdateUser(guid, login, password, name):
    connection = GetConnection()
    query = """UPDATE "Users" SET"""
    if login is not None:
        query = query + f""" "Login" = '{login}'"""
    if password is not None:
        query = query + f""" "Password" = '{password}'"""
    if name is not None:
        query = query + f""" "Name" = '{name}'"""

    query = query + f""" WHERE "Guid" = '{guid}'"""
    query = query.replace("""' \"""", """', \"""")
    with connection.cursor() as cursor:
        cursor.execute(query)
    connection.close()


def UpdateMachine(guid, host, port, password):
    connection = GetConnection()
    query = """UPDATE "Users" SET"""
    if host is not None:
        query = query + f""" "Host" = '{host}'"""
    if password is not None:
        query = query + f"""" "Password" = '{password}'"""
    if port is not None:
        query = query + f"""" "Port" = '{port}'"""

    query = query + f"""" WHERE "Guid" = '{guid}'"""
    query = query.replace("""' \"""", """', \"""")
    with connection.cursor() as cursor:
        cursor.execute(query)
    connection.close()


def DeleteUser(guid):
    connection = GetConnection()
    with connection.cursor() as cursor:
        cursor.execute(f"""DELETE FROM "Users" WHERE "Guid" = '{guid}'""")
    connection.close()


def DeleteMachine(guid):
    connection = GetConnection()
    with connection.cursor() as cursor:
        cursor.execute(f"""DELETE FROM "UserToMachine" WHERE "Guid" = '{guid}'""")
    connection.close()


def GetUserMachine(machineName, user, password):
    connection = GetConnection()
    with connection.cursor() as cursor:
        cursor.execute(f"""SELECT * FROM "UserToMachine"
        WHERE "MachineName" = '{machineName}' AND "User" = '{user}'
        AND "Password" = '{password}'""")
        response = cursor.fetchone()

    result = UserToMachine(
        guid=response[0],
        userGUID=response[1],
        machineName=response[2],
        host=response[3],
        user=response[4],
        password=response[5],
        port=response[6]
    )
    connection.close()
    return result


def GetUser(login):
    connection = GetConnection()
    with connection.cursor() as cursor:
        cursor.execute(f"""SELECT * FROM "Users" WHERE "Login" = '{login}'""")
        response = cursor.fetchone()

    result = Users(
        login=response[1],
        password=response[2],
        name=response[3]
    )
    connection.close()
    return result

def GetHistory(userGUID):
    connection = GetConnection()
    with connection.cursor() as cursor:
        cursor.execute(f"""SELECT "h"."Guid", "h"."MachineGUID", "h"."Command", "h"."Time"
        FROM "UserToMachine" u JOIN "HistoryToMachine" h ON "u"."Guid" = "h"."MachineGUID"
        WHERE "u"."UserGUID" = '{userGUID}'""")
        response = cursor.fetchall()
    result = [
        HistoryToMachine(
            guid=row[0],
            machineGUID=row[1],
            command=row[2],
            time=row[3]
        ) for row in response
    ]
    connection.close()
    return result


def GetCommandToUser():
    # result = mycursor.execute("SELECT * FROM CommandToUser")
    # return [list(i) for i in result]
    return 1
