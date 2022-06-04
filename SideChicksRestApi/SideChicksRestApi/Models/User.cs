using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace SideChicksRestApi.Models;

public class User: IdentityUser
{
    [PersonalData]
    public string? FirstName { get; set; }
    
    [PersonalData]
    public string? LastName { get; set; }
    
    [PersonalData]
    public string? Instrument { get; set; }
    
    [PersonalData]
    public bool IsAdmin { get; set; }
    [PersonalData]
    public string Password { get; set; }
    

}