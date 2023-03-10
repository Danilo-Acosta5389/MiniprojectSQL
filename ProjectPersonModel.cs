using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniprojectSQL
{
    public class ProjectPersonModel
    {
        //I had no choice but to use this object in order to make changes to the project_person table in the DB
        //A method in DatabaseAccess.cs takes this in as Lis<ProjectPersonModel>
        //I had to add project_name and person_name here aswell so that there where not only numbers in the listing when editing registered hours


        public int id { get; set; }
        public int reg_person_id { get; set; }
        public int reg_project_id { get; set; }
        public string project_name { get; set; }
        public string person_name { get; set; }
        public int hours { get; set; }
    }
}
