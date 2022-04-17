--
-- PostgreSQL database dump
--

-- Dumped from database version 12.9 (Ubuntu 12.9-0ubuntu0.20.04.1)
-- Dumped by pg_dump version 12.9 (Ubuntu 12.9-0ubuntu0.20.04.1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: HistoryToMachine; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."HistoryToMachine" (
    guid uuid NOT NULL,
    "MachineGUID" uuid NOT NULL,
    "Command" character varying NOT NULL,
    "Time" date NOT NULL
);


ALTER TABLE public."HistoryToMachine" OWNER TO postgres;

--
-- Name: UserToMachine; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."UserToMachine" (
    guid uuid NOT NULL,
    "UserGUID" uuid NOT NULL,
    "MachineName" character varying NOT NULL,
    "Host" character varying NOT NULL,
    "User" character varying NOT NULL,
    "Password" character varying NOT NULL,
    "Port" integer NOT NULL
);


ALTER TABLE public."UserToMachine" OWNER TO postgres;

--
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    "Guid" uuid NOT NULL,
    "Login" character varying NOT NULL,
    "Password" character varying NOT NULL,
    "Name" character varying NOT NULL
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- Data for Name: HistoryToMachine; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."HistoryToMachine" (guid, "MachineGUID", "Command", "Time") FROM stdin;
\.


--
-- Data for Name: UserToMachine; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."UserToMachine" (guid, "UserGUID", "MachineName", "Host", "User", "Password", "Port") FROM stdin;
\.


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Users" ("Guid", "Login", "Password", "Name") FROM stdin;
\.


--
-- Name: HistoryToMachine historytomachine_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."HistoryToMachine"
    ADD CONSTRAINT historytomachine_pk PRIMARY KEY (guid);


--
-- Name: Users users_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT users_pk PRIMARY KEY ("Guid");


--
-- Name: UserToMachine usertomachine_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserToMachine"
    ADD CONSTRAINT usertomachine_pk PRIMARY KEY (guid);


--
-- Name: historytomachine_guid_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX historytomachine_guid_uindex ON public."HistoryToMachine" USING btree (guid);


--
-- Name: users_guid_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX users_guid_uindex ON public."Users" USING btree ("Guid");


--
-- Name: users_login_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX users_login_uindex ON public."Users" USING btree ("Login");


--
-- Name: usertomachine_guid_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX usertomachine_guid_uindex ON public."UserToMachine" USING btree (guid);


--
-- Name: HistoryToMachine historytomachine_usertomachine_guid_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."HistoryToMachine"
    ADD CONSTRAINT historytomachine_usertomachine_guid_fk FOREIGN KEY ("MachineGUID") REFERENCES public."UserToMachine"(guid) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: UserToMachine usertomachine_users_guid_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserToMachine"
    ADD CONSTRAINT usertomachine_users_guid_fk FOREIGN KEY ("UserGUID") REFERENCES public."Users"("Guid") ON UPDATE CASCADE ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

