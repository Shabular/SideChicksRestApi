using System.ComponentModel.DataAnnotations;

namespace SideChicksRestApi.Helpers;

public class EmailHelper
{
    public static string userMailCheck(string email)
    {
        if(email == null){
            return "Not eligable";
        }
        if(new EmailAddressAttribute().IsValid(email))
        {
            return email;
        }
        else
        {

            return "Not eligable";
        }
    }
}