using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniprojectSQL
{
    public class PersonModel
    {
        //This model resembles the person table in the SQL database
        //This can be later called for listing and changing, however i berely used this
        //It might however come in handy if i decide to change some of the structure in the methods that make calls to the DB.

        public int id { get; set; }
        public string person_name { get; set; }
    }
}
