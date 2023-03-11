# MiniprojectSQL
<p align="center">Or as i call it</p>
<div align="center">
  <img src="https://user-images.githubusercontent.com/113366808/224378996-3bd342cc-9bef-4375-812e-91032252aecb.gif" width="800"   alt="WorkTimeTracker-logo"/>
</div>
</br>

## About
This is the WorkTimeTracker app,
it's an app to track hours spent on a project. You can add a project, add your name and later register hours spent on a project. You may edit project and person names, you can also change amount of hours entered at some point.
Deletion is possible but is limited (for now). If one has registered hours on a person or a project it is not possible to delete that person or project (yet).
</br>
</br>
This was created as a school asignment. The programming languages used where C# and SQL.


## Functionality
<img src="https://user-images.githubusercontent.com/113366808/224426739-20e1cc0c-2a47-4d4f-ba07-ce49033129cc.png" width="200" align="right">
The functions currently in place are:
<ul>
  <li>See all registered hours - "Work Time Tracker"</li>
  <li>Register hours</li>
  <li>Add new project</li>
  <li>Add new person</li>
  <li>Edit registered time</li>
  <li>Edit project name</li>
  <li>Edit person name</li>
  <li>Delete project</li>
  <li>Delete person</li>
  <li>Close application</li>
</ul>


### Known bugs and TODO
Only bug that i can think of is in the SQL database. If the sequence of the auto incremented column, the "id" column, loses it's order because one might have manually deleted a table row a bug may occure where it might start auto incrementing from the id that has been lost. This drove me nuts for a moment. I'm not sure if i managed to fix it.

Otherwise i have a TODO list for future changes:
<ul>
  <li>Add DateTime</li>
  <li>Be able to enter minutes</li>
  <li>Add "Danger zone" option. This should contain deletion of all sorts. Including whiping all data of the DB, to start over completely.</li>
  <li>User should be able to delete a wrongful entry. At the moment user can only change amount of hours in an entry.</li>
</ul>

## How to setup
In order to run this, you will need an editor, i used Visual Studios 2022. You will need three nuget packages:
<ul>
  <li>Dapper(2.0.123)</li>
  <li>Npgsql(7.0.1)</li>
  <li>System.configuration.configurationManager(7.0.0)</li>
</ul>
You will need an application configuration file that contains the connectionString. Mine is not in the repo, you can see in the .gitignore file that app.config is being ignored, well thats why it isn't there. You could look in this <a href="https://www.youtube.com/watch?v=Et2khGnrIqc">Tim Corey youtube video</a>  when he makes an app.config file and adds a connectionString <a href="https://www.youtube.com/watch?v=Et2khGnrIqc">from this website.</a>

## SQL

This is a SQL exercise, i used Postgresql Database to store and get data from. A SQL dump is provided in the code. There are three tables, dac_person, dac_project and dac_project_person (obviusly you could rename these anything you want). The whole point is that dac_project_person table has foreign keys to the other two tables so in other words there is a many to many relationship like shown in the image below.
</br></br>
<div align="center">
<img src="https://user-images.githubusercontent.com/113366808/224417422-f86f1f4f-6860-4938-9578-8317e292d8ba.png" width="500" alt="image of SQL structure and relationships">
</div>
</br>
</br>
Like mentioned above, you will need three tables with foregin keys.
Here is a snippet on the base SQL code to recreate those same tables including the foreign keys.
NOTE: I was using PostgreSQL.
</br>
</br>

```
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

ALTER TABLE "public"."dac_project_person" ADD CONSTRAINT "FK_dac_project_person_project_id" FOREIGN KEY ("project_id") REFERENCES "public"."dac_project" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."dac_project_person" ADD CONSTRAINT "FK_dac_person_project_person_id" FOREIGN KEY ("person_id") REFERENCES "public"."dac_person" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
```
</br>

## Demo of the app

</br>
<div align="center">
  <img src="https://user-images.githubusercontent.com/113366808/224427749-824d3ff1-7f03-4096-bde9-9fd7e18ebeb1.gif" width="900" alt="Gif showing the app">
</div>
