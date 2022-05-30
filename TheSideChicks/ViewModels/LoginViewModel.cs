using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using TheSideChicks.View;

namespace TheSideChicks.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {

        UserService userService;
        ShowService showService;
        LocationService locationService;
  

        public ObservableCollection<User> User { get; } = new();

        public string id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string instrument { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public bool isadmin { get; set; }

        public System.Windows.Input.ICommand LogIn { get; }

        public LoginViewModel(UserService userService, ShowService showService, LocationService locationService)
        {
            Title = "Log in";

            LogIn = new Command(LogInAsync);

            this.userService = userService;
            this.showService = showService;
            this.locationService = locationService;

        }

        private async void LogInAsync()
        {

            var user = new User
            {
                username = username,
                password = password,
            };

            try
            {
                var userInDatabase = await userService.CheckIfUserInDatabase(user);
                Preferences.Set("username", userInDatabase.username);
                Preferences.Set("isAdmin", userInDatabase.isadmin);

                UserViewModel userViewModel = new(userService, showService, locationService);
                

                // check if user already exists, if not add location
                if (Preferences.Get("isAdmin", false).Equals(true))
                {

                    await Shell.Current.GoToAsync(nameof(MembersPage), true,

                        new Dictionary<string, object>
                        {
                            { "UserViewModel", userViewModel }
                        });
                }
            }
            catch
            {
                await Shell.Current.DisplayAlert("Account not allowed", $"Try other credentials if you dare", "OK");
                return;
            }
            
            

            

            

        }
    }
}

