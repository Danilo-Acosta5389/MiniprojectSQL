using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniprojectSQL
{
    public class ProjectPersonModel
    {
        //THIS OBJECT RESEMBLES THE DATABASE TABLE dac_project_person
        //Methods usualy uses this as type, in DatabaseAccess.cs method may use this as list type List<ProjectPersonModel>
        //I had to add project_name and person_name here aswell so that there where not only numbers in the listing when editing registered hours


        public int id { get; set; }
        public int reg_person_id { get; set; }
        public int reg_project_id { get; set; }
        public string project_name { get; set; }
        public string person_name { get; set; }
        public int hours { get; set; }

        public string extra { get; set; } //Another extra (no pun intended) feature here is the extra-field, I will store strings like "Back to menu" here
    }
}
