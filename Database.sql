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
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (1, 'ChatApp');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (2, 'Analytical_Eng');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (3, 'Turing_Machine');
INSERT INTO "public"."dac_project" ("id", "project_name") VALUES (4, 'Harvard_Mark_I');
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (1, 1, 1, 8);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (2, 1, 1, 8);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (3, 1, 1, 13);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (4, 1, 1, 7);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (5, 3, 2, 12);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (6, 3, 2, 12);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (7, 3, 2, 13);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (8, 3, 2, 9);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (9, 3, 2, 7);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (10, 2, 3, 13);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (11, 2, 3, 12);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (12, 2, 3, 12);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (13, 2, 4, 13);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (14, 2, 4, 12);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (15, 2, 4, 10);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (16, 2, 4, 9);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (17, 2, 3, 7);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (18, 2, 4, 7);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (19, 4, 5, 12);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (20, 4, 5, 13);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (21, 4, 5, 10);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (22, 4, 5, 9);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (23, 4, 5, 7);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (24, 1, 1, 8);
INSERT INTO "public"."dac_project_person" ("id", "project_id", "person_id", "hours") VALUES (25, 3, 1, 8);
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (1, 'Danilo');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (2, 'Alan');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (3, 'Charles');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (4, 'Ada');
INSERT INTO "public"."dac_person" ("id", "person_name") VALUES (5, 'Grace');
ALTER TABLE "public"."dac_project_person" ADD CONSTRAINT "FK_dac_project_person_project_id" FOREIGN KEY ("project_id") REFERENCES "public"."dac_project" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."dac_project_person" ADD CONSTRAINT "FK_dac_person_project_person_id" FOREIGN KEY ("person_id") REFERENCES "public"."dac_person" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
