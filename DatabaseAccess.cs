using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Npgsql;


namespace MiniprojectSQL
{
    public class DatabaseAccess
    {

        /*
         * This class contains all the methods that make calls to the Database
         * 
         * These methods do things like getting names and id's from the dac_person table, the dac_project table or the dac_project_person table
         * aswell as updating, inserting and deleting
         * 
         */

        //THIS METHOD GETS ONLY ONE PERSON ON ONE SPECIFIC PROJECT FROM THE DATABASE
        public static List<ProjectPersonModel> GetPersonOnOneProject(int person_id, int project_id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectPersonModel>(@$"
                        SELECT dac_project_person.id, dac_person.person_name,
                        dac_project.project_name, dac_project_person.hours
                        FROM dac_project_person 
                        LEFT JOIN dac_person
                        ON  dac_project_person.person_id = dac_person.id 
                        LEFT JOIN dac_project
                        ON dac_project_person.project_id = dac_project.id
                        WHERE dac_person.id = {person_id}
                        AND dac_project.id = {project_id} ORDER BY dac_project_person.id ASC;", new DynamicParameters());
                return output.ToList();
            }
        }


        //THIS METHOD GETS ONE PERSON THAT HAS REGISTERED HOURS IN SEVERAL PROJECTS
        public static List<ProjectPersonModel> GetPersonWithManyProjects(int person_id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectPersonModel>(@$"
                        SELECT dac_project_person.id, dac_person.person_name,
                        dac_project.project_name, dac_project_person.hours
                        FROM dac_project_person 
                        LEFT JOIN dac_person
                        ON  dac_project_person.person_id = dac_person.id 
                        LEFT JOIN dac_project
                        ON dac_project_person.project_id = dac_project.id
                        WHERE dac_person.id = {person_id} ORDER BY dac_project_person.id ASC;", new DynamicParameters());
                return output.ToList();
            }
        }


        //THIS METHOD GETS ONE PROJECT WITH MANY PERSONS WHO HAVE REGISTERED HOURS ON IT
        public static List<ProjectPersonModel> GetProjectWithMany(int project_id)
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectPersonModel>(@$"
                            SELECT dac_project_person.id, dac_project.project_name , dac_person.person_name
                            , dac_project_person.hours
                            FROM dac_project_person 
                            LEFT JOIN dac_project
                            ON dac_project_person.project_id = dac_project.id
                            LEFT JOIN dac_person
                            ON  dac_project_person.person_id = dac_person.id 
                            WHERE dac_project.id = {project_id} ORDER BY dac_project_person.id ASC;", new DynamicParameters());
                return output.ToList();
            }
        }

        //THIS METHOD GETS ALL THE PROJECTS AND THE PERSONS BUT DOESEN'T LIST IT LIKE THE DATABASE LISTS IT, INSTEAD OF SHOWING project_name.id AND person_name.id
        //IT WILL SHOW THE ACTUAL project_name, person_name and hours, SINCE IT IS THE MOST RELEVANT INFORMATION FOR THE USER.
        public static List<ProjectPersonModel> GetProjectPersonList()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectPersonModel>(@"
                        SELECT dac_project_person.id, dac_project.project_name,
                                dac_person.person_name, dac_project_person.hours
                        FROM dac_project_person
                        LEFT JOIN dac_project
                        ON  dac_project_person.project_id = dac_project.id
                        LEFT JOIN dac_person
                        ON dac_project_person.person_id = dac_person.id ORDER BY dac_project_person.id ASC", new DynamicParameters());
                return output.ToList();

            }
        }

        //SOME OF THE METHODS RETURN A BOOLEAN TO INDICATE IF THE OPERATION WAS SUCCESSFUL OR NOT


        //THIS METHOD WILL BE CALLED BY ANOTHER METHOD TO PERFORM DELETION OF PROJECT BY ID IN THE DATABASE
        public static bool DeleteProjectName(int id)
        {
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute($"DELETE FROM dac_project WHERE id = {id};");
                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }


        //THIS METHOD WILL BE CALLED BY ANOTHER METHOD TO PERFORM DELETION OF PERSON BY ID IN THE DATABASE
        public static bool DeletePersonName(int id)
        {
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute($"DELETE FROM dac_person WHERE id = {id}; ");
                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }

        //THIS METHOD PERFORMES AN UPDATE ON A ROW ON dac_project_person TABLE IN THE DATABASE
        public static bool EditRegisteredHours(int id, int hours)
        {
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute($"UPDATE dac_project_person SET hours = {hours} WHERE id = {id};");
                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }


        //THIS METHOD UPDATES ROW ON dac_project TABLE IN DATABASE BY ID
        public static bool EditProjectName(int id, string newName)
        {
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute($"UPDATE dac_project SET project_name = '{newName}' WHERE id = {id}"); //NEW NAME WILL BE INSERTED WHERE ID IS LOCATED
                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }

        
        //THIS METHOD UPDATES ROW ON dac_person TABLE IN DATABASE BY ID
        public static bool EditPersontName(int id, string newName)
        {
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute($"UPDATE dac_person SET person_name = '{newName}' WHERE id =  {id}");

                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }



        //THIS METHOD WILL UPDATE hours ON A ROW ON dac_project_person TABLE BY THE CORRECT ID's
        public static bool RegistrateHoursInDB(int project_id, int person_id, int hours)
        {
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute(@$"
                          BEGIN;
                              INSERT INTO dac_project_person (project_id, person_id, hours) 
                              VALUES 
                              ({project_id} , 
                              {person_id} , {hours});
                          COMMIT;");
                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }


        //THE METHOD BELOW GETS ALL PROJECT NAMES AND ID'S FROM dac_project TABLE AND USES ProjectModel AS A LIST TYPE TO LATER DISPLAY
        public static List<ProjectModel> GetProjectTable()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectModel>("SELECT * FROM dac_project ORDER BY id ASC", new DynamicParameters());
                return output.ToList();
            }
        }
        //LIKE THE ABOVE CODE, THIS METHOD DOES EXACLTY THE SAME BUT FOR PERSONS FROM dac_person TABLE
        public static List<PersonModel> GetPersonTable()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PersonModel>("SELECT * FROM dac_person ORDER BY id ASC", new DynamicParameters());
                return output.ToList();
            }
        }




        //THE BELOW CODE INSERTS A NEW NAME INTO THE TABLE dac_project IN DATABASE
        public static bool InsertNewProject(string projectName)
        {
            if (projectName == string.Empty) return false;
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute($"insert into dac_project (project_name) values ('{projectName}')");
                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }


        //THE BELOW CODE INSERTS A NEW NAME INTO THE TABLE dac_person IN DATABASE
        public static bool InsertNewPerson(string personName)
        {
            if (personName == string.Empty) return false;
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute($"insert into dac_person (person_name) values ('{personName}')");
                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }


        //THIS METHOD LOADS THE CONNECTIONSTRING IN THE APP.CONFIG FILE, THE SAME STRING IS LATER PASSED ON TO THE OTHER METHODS IN THIS CALSS
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
