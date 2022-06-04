using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.Models
{
    public class User
    {
        public string id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string instrument { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public List<int> locationids{ get; set; }

        public bool isadmin { get; set; }


    }
}
