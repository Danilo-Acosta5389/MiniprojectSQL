using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniprojectSQL
{
    public class ProjectModel
    {
        //This model resembles the dac_project table in the SQL database
        //This is used when listing project names in navigation menu
        //It is also used by methods that make calls to the Db, to get all users, it is often used as a list type List<ProjectModel>

        public int id { get; set; }
        public string project_name { get; set; }
        public string extra { get; set; } //Go back option goes in here when listing in menu
    }
}
