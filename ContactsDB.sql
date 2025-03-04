--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4
-- Dumped by pg_dump version 17.4

-- Started on 2025-03-02 19:07:26

-- SET statement_timeout = 0;
-- SET lock_timeout = 0;
-- SET idle_in_transaction_session_timeout = 0;
-- SET transaction_timeout = 0;
-- SET client_encoding = 'UTF8';
-- SET standard_conforming_strings = on;
-- SELECT pg_catalog.set_config('search_path', '', false);
-- SET check_function_bodies = false;
-- SET xmloption = content;
-- SET client_min_messages = warning;
-- SET row_security = off;

-- DROP DATABASE "Contacts";
-- --
-- -- TOC entry 4801 (class 1262 OID 16551)
-- -- Name: Contacts; Type: DATABASE; Schema: -; Owner: postgres
-- --

-- CREATE DATABASE "Contacts" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en-US';


-- ALTER DATABASE "Contacts" OWNER TO postgres;

-- \connect "Contacts"

-- SET statement_timeout = 0;
-- SET lock_timeout = 0;
-- SET idle_in_transaction_session_timeout = 0;
-- SET transaction_timeout = 0;
-- SET client_encoding = 'UTF8';
-- SET standard_conforming_strings = on;
-- SELECT pg_catalog.set_config('search_path', '', false);
-- SET check_function_bodies = false;
-- SET xmloption = content;
-- SET client_min_messages = warning;
-- SET row_security = off;

-- --
-- -- TOC entry 4 (class 2615 OID 2200)
-- -- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
-- --

-- CREATE SCHEMA public;


-- ALTER SCHEMA public OWNER TO pg_database_owner;

-- --
-- -- TOC entry 4802 (class 0 OID 0)
-- -- Dependencies: 4
-- -- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
-- --

-- COMMENT ON SCHEMA public IS 'standard public schema';


-- SET default_tablespace = '';

-- SET default_table_access_method = heap;

--
-- TOC entry 217 (class 1259 OID 16552)
-- Name: Contacts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Contacts" (
    contact_id uuid NOT NULL,
    name character varying(50) NOT NULL,
    phone_no character varying,
    email_adddress character varying(150),
    title character varying(50),
    post_address character varying(100),
    post_code character varying(50),
    created_by character varying(50) NOT NULL,
    created_at date NOT NULL,
    last_modified_by character varying(50),
    last_modified_at date,
    customer_id uuid
);


ALTER TABLE public."Contacts" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16559)
-- Name: Customers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Customers" (
    customer_id uuid NOT NULL,
    organization_name character varying(50) NOT NULL,
    created_by character varying(50),
    created_at date,
    last_modified_by character varying(50),
    last_modified_at date
);


ALTER TABLE public."Customers" OWNER TO postgres;

--
-- TOC entry 4794 (class 0 OID 16552)
-- Dependencies: 217
-- Data for Name: Contacts; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 4795 (class 0 OID 16559)
-- Dependencies: 218
-- Data for Name: Customers; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Customers" VALUES ('3fa85f64-5717-4562-b3fc-2c963f66afa6', 'NHIF', 'david', '2025-03-02', 'david', '2025-03-02');


--
-- TOC entry 4645 (class 2606 OID 16556)
-- Name: Contacts Contacts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Contacts"
    ADD CONSTRAINT "Contacts_pkey" PRIMARY KEY (contact_id);


--
-- TOC entry 4647 (class 2606 OID 16563)
-- Name: Customers Customer_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Customers"
    ADD CONSTRAINT "Customer_pkey" PRIMARY KEY (customer_id);


--
-- TOC entry 4648 (class 2606 OID 16564)
-- Name: Contacts customer_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Contacts"
    ADD CONSTRAINT customer_id FOREIGN KEY (customer_id) REFERENCES public."Customers"(customer_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


-- Completed on 2025-03-02 19:07:26

--
-- PostgreSQL database dump complete
--

