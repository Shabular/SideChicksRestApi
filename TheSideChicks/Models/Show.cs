using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.Models
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
/*        
        public Location Location { get; set; }
        public Band Band { get; set; }*/
        public DateTime Date{ get; set; }
        public bool Accepted { get; set; }

    }
}
