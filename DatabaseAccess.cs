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

        public static bool DeleteProjectName(string projectName)
        {
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute($"DELETE FROM dac_project WHERE project_name = '{projectName}';");
                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }

        public static bool DeletePersonName(string personName)
        {
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute($"DELETE FROM dac_person WHERE person_name = '{personName}'; ");
                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }


        public static bool EditProjectName(string oldName, string newName)
        {
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute($"UPDATE dac_project SET project_name = '{newName}' WHERE project_name = '{oldName}'");
                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }


        public static bool EditPersontName(string oldName, string newName)
        {
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute($"UPDATE dac_person SET person_name = '{newName}' WHERE person_name = '{oldName}'");

                }
            }
            catch (Exception e)
            {
                General.ErrorInRed(e.Message);
                return false;
            }
            return true;
        }


        public static bool RegistrateHoursInDB(string project_name, string person_name, int hours)
        {
            try
            {
                using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
                {
                    cnn.Execute(@$"
                          BEGIN;
                              INSERT INTO dac_project_person (project_id, person_id, hours) 
                              VALUES 
                              ((SELECT id FROM dac_project WHERE project_name = '{project_name}') , 
                              (SELECT id FROM dac_person WHERE person_name = '{person_name}') , {hours});
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

        public static List<string> GetProjectName()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<string>("SELECT project_name FROM dac_project", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<string> GetPersonName()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<string>("SELECT person_name FROM dac_person", new DynamicParameters());
                return output.ToList();
            }
        }


        public static List<ProjectModel> GetProject()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ProjectModel>("SELECT * FROM dac_project", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<PersonModel> GetPerson()
        {
            using (IDbConnection cnn = new NpgsqlConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PersonModel>("SELECT * FROM dac_person", new DynamicParameters());
                return output.ToList();
            }
        }

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

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
