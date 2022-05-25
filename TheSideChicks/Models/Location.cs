using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string VenueName { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string PostalNumber { get; set; }
        public int PhoneNumber { get; set; }
        public string email { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateUntill { get; set; }
        public double Fee { get; set; }
        public string Photo { get; set; }


    }
}
