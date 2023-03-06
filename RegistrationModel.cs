using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniprojectSQL
{
    public class RegistrationModel
    {
        public int id { get; set; }
        public int reg_person_id { get; set; }
        public int reg_project_id { get; set; }
        public int hours { get; set; }
    }
}
