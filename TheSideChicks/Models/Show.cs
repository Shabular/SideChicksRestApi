using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.Models
{
    internal class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Location Location { get; set; }
        public Band Band { get; set; }
        public string Image { get; set; }


    }
}
