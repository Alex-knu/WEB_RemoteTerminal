CREATE DATABASE "RemoteTerminal";

CREATE TABLE "HistoryToMachine" (
    "Guid" uuid NOT NULL,
    "MachineGUID" uuid NOT NULL,
    "Command" character varying NOT NULL,
    "Time" date NOT NULL
);

CREATE TABLE "UserToMachine" (
    "Guid" uuid NOT NULL,
    "UserGUID" uuid NOT NULL,
    "MachineName" character varying NOT NULL,
    "Host" character varying NOT NULL,
    "User" character varying NOT NULL,
    "Password" character varying NOT NULL,
    "Port" integer NOT NULL
);

CREATE TABLE "Users" (
    "Guid" uuid NOT NULL,
    "Login" character varying NOT NULL,
    "Password" character varying NOT NULL,
    "Name" character varying NOT NULL
);

ALTER TABLE ONLY "HistoryToMachine"
    ADD CONSTRAINT historytomachine_pk PRIMARY KEY ("Guid");

ALTER TABLE ONLY "Users"
    ADD CONSTRAINT users_pk PRIMARY KEY ("Guid");

ALTER TABLE ONLY "UserToMachine"
    ADD CONSTRAINT usertomachine_pk PRIMARY KEY ("Guid");

CREATE UNIQUE INDEX historytomachine_guid_uindex ON "HistoryToMachine" USING btree ("Guid");

CREATE UNIQUE INDEX users_guid_uindex ON "Users" USING btree ("Guid");

CREATE UNIQUE INDEX users_login_uindex ON "Users" USING btree ("Login");

CREATE UNIQUE INDEX usertomachine_guid_uindex ON "UserToMachine" USING btree ("Guid");

ALTER TABLE ONLY "HistoryToMachine"
    ADD CONSTRAINT historytomachine_usertomachine_guid_fk FOREIGN KEY ("MachineGUID") REFERENCES "UserToMachine"("Guid") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "UserToMachine"
    ADD CONSTRAINT usertomachine_users_guid_fk FOREIGN KEY ("UserGUID") REFERENCES "Users"("Guid") ON UPDATE CASCADE ON DELETE CASCADE;

