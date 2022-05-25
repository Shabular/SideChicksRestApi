using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.Models
{
    internal class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BandMember> Member { get; set; }
        public string Photo { get; set; }
    }
}
