using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.Models
{
    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
        public string street { get; set; }
        public int number { get; set; }
        public string postalNumber { get; set; }
        public int phoneNumber { get; set; }
        public string email { get; set; }
        public string adress { get; set; }

        public string contactInfo { get; set; }


        public void SetPropperty(string prop, string val)
        {
        }

    }
}
