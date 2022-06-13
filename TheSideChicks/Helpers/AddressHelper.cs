using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheSideChicks.Helpers
{
    internal class AddressHelper
    {
        

        public static string isDutchPostalCode(string postalCode)
        {
            const string Pattern = @"^[0-9]{4}([A-Za-z]{2})?$"; 

            Regex rx = new Regex(Pattern,
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (rx.IsMatch(postalCode))
               return postalCode;
            else
                return "NotEligable";
        }
    }
    
}
