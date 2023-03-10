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
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (13, 'PiedPiper');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (14, 'Aviato');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (15, 'Foogle');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (17, 'Chatbot');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (18, 'ChatApp');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (21, 'SpaceY');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (22, 'Analytical_Eng');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (23, 'Turing_Machine');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (25, 'Harvard_Mark_I');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (19, 'pc_game');
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (103, 13, 1, 8);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (104, 13, 13, 8);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (105, 13, 14, 8);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (106, 15, 15, 8);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (107, 22, 19, 8);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (108, 22, 22, 6);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (109, 17, 20, 12);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (110, 25, 21, 12);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (111, 25, 21, 9);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (112, 25, 21, 10);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (113, 25, 21, 7);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (114, 23, 23, 10);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (115, 23, 23, 16);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (116, 23, 23, 12);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (117, 23, 23, 8);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (118, 22, 19, 7);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (119, 22, 22, 12);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (120, 18, 1, 6);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (121, 18, 13, 6);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (122, 14, 13, 10);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (123, 14, 13, 8);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (124, 14, 13, 7);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (125, 14, 13, 9);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (126, 17, 14, 10);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (127, 21, 20, 10);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (128, 21, 20, 12);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (129, 21, 20, 9);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (130, 22, 19, 9);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (131, 22, 22, 9);
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (13, 'Pablo');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (14, 'Mehmet');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (15, 'Pascal');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (19, 'Ada');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (20, 'Cardano');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (21, 'Grace');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (22, 'Charles');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (23, 'Alan');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (1, 'Danilo');
ALTER TABLE "public"."dac_project_person" ADD CONSTRAINT "FK_dac_project_person_project_id" FOREIGN KEY ("project_id") REFERENCES "public"."dac_project" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."dac_project_person" ADD CONSTRAINT "FK_dac_person_project_person_id" FOREIGN KEY ("person_id") REFERENCES "public"."dac_person" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
