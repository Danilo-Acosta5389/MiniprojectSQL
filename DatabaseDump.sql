ALTER TABLE "public"."dac_project_person" DROP CONSTRAINT "FK_dac_project_person_project_id";
ALTER TABLE "public"."dac_project_person" DROP CONSTRAINT "FK_dac_person_project_person_id";
DROP TABLE IF EXISTS "public"."dac_project";
DROP TABLE IF EXISTS "public"."dac_project_person";
DROP TABLE IF EXISTS "public"."dac_person";
CREATE TABLE "public"."dac_project" ( 
  "id" SERIAL,
  "project_name" VARCHAR(50) NOT NULL,
  CONSTRAINT "dac_project_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."dac_project_person" ( 
  "id" SERIAL,
  "project_id" INTEGER NOT NULL,
  "person_id" INTEGER NOT NULL,
  "hours" INTEGER NOT NULL,
  CONSTRAINT "dac_project_person_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."dac_person" ( 
  "id" SERIAL,
  "person_name" VARCHAR(25) NOT NULL,
  CONSTRAINT "dac_person_pkey" PRIMARY KEY ("id")
);
INSERT INTO "public"."dac_project" ("project_name") VALUES ('PiedPiper');
INSERT INTO "public"."dac_project" ("project_name") VALUES ('Aviato');
INSERT INTO "public"."dac_project" ("project_name") VALUES ('Foogle');
INSERT INTO "public"."dac_project" ("project_name") VALUES ('DunderMifflin');
INSERT INTO "public"."dac_project" ("project_name") VALUES ('Chatbot');
INSERT INTO "public"."dac_project" ("project_name") VALUES ('ChatApp');
INSERT INTO "public"."dac_project" ("project_name") VALUES ('pc_game');
INSERT INTO "public"."dac_project" ("project_name") VALUES ('Flying_car');
INSERT INTO "public"."dac_project" ("project_name") VALUES ('SpaceY');
INSERT INTO "public"."dac_project" ("project_name") VALUES ('Analytical_Eng');
INSERT INTO "public"."dac_project" ("project_name") VALUES ('Turing_Machine');
INSERT INTO "public"."dac_project" ("project_name") VALUES ('Android-game');
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (4, 12, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (4, 34, 7);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (19, 32, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (18, 1, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (19, 12, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (20, 36, 32);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (21, 35, 22);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (5, 1, 5);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (18, 36, 7);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (11, 1, 5);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (18, 35, 5);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (20, 32, 5);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (4, 1, 4);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (4, 1, 5);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (20, 30, 10);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (19, 1, 10);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (23, 40, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (23, 40, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (23, 35, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (23, 35, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (19, 12, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (20, 12, 7);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (24, 12, 7);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (29, 42, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (29, 42, 12);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (29, 42, 10);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (29, 42, 7);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (21, 1, 10);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (29, 42, 20);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (5, 1, 9);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (5, 30, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (4, 31, 9);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (11, 1, 8);
INSERT INTO "public"."dac_project_person" ("project_id", "person_id", "hours") VALUES (19, 12, 7);
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Pablo');
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Mehmet');
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Pascal');
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Shirin');
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Maria');
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Dania');
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Ada');
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Cardano');
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Grace');
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Charles');
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Alan');
INSERT INTO "public"."dac_person" ("person_name") VALUES ('Danilo123');
ALTER TABLE "public"."dac_project_person" ADD CONSTRAINT "FK_dac_project_person_project_id" FOREIGN KEY ("project_id") REFERENCES "public"."dac_project" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."dac_project_person" ADD CONSTRAINT "FK_dac_person_project_person_id" FOREIGN KEY ("person_id") REFERENCES "public"."dac_person" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
