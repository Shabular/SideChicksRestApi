using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.Models
{
    public class Show
    {

        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public int locationId { get; set; }
        public double fee { get; set; }
        public DateTime date{ get; set; }
        public bool accepted { get; set; }

    }
}
