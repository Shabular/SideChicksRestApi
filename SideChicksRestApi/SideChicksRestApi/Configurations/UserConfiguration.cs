using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SideChicksRestApi.Controllers;
using SideChicksRestApi.Data;
using SideChicksRestApi.Models;
using Microsoft.EntityFrameworkCore;
using SideChicksRestApi.Configurations;
using SideChicksRestApi.Data;
using SideChicksRestApi.Models;

namespace SideChicksRestApi.Configurations;

public class UserConfiguration
{

    public async void Configure(UserController userController)
    {
        var users = await userController.Get();
        if (users.Count == 0)
        {
            var admin = new User
            {
                UserName = "admin",
                Password = "Welkom01!"
            };

            userController.Create(admin);
        }
    }
}