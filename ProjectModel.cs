using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniprojectSQL
{
    public class ProjectModel
    {
        //I barely use this but might come in handy if i change the structure of the methods that make calls to the DB
        //The id is a uniqe identifier that i should use more often in the program
        //However i almost exclusevly use the "project_name" variable/field.

        public int id { get; set; }
        public string project_name { get; set; }
    }
}
