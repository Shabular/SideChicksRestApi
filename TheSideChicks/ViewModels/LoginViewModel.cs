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
        NewsService newsService;


        public ObservableCollection<User> User { get; } = new();

        public string id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string instrument { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public bool isadmin { get; set; }

        public System.Windows.Input.ICommand LogIn { get; }
        public System.Windows.Input.ICommand Register { get; }

        public LoginViewModel(UserService userService, ShowService showService, LocationService locationService, NewsService newsService)
        {
            Title = "Log in";

            LogIn = new Command(LogInAsync);
            Register = new Command(RegisterAsync);

            this.userService = userService;
            this.showService = showService;
            this.locationService = locationService;
            this.newsService = newsService;
        }

        public async void LogInAsync()
        {

            var user = new User
            {
                username = username,
                password = password,
            };

            try
            {
                var userInDatabase = await userService.CheckIfUserInDatabase(user);
                if (userInDatabase.id == null)
                {
                    await Shell.Current.DisplayAlert("Nice trye", $"Thats not you password", "OK");
                    return;
                }
                   
                Preferences.Set("username", userInDatabase.username);
                Preferences.Set("isAdmin", userInDatabase.isadmin);
                Preferences.Set("userId", userInDatabase.id);
                Preferences.Set("userEmail", userInDatabase.email);
                Preferences.Set("isLoggedIn", true);

                UserViewModel userViewModel = new(userService, showService, locationService, newsService);


                await Shell.Current.GoToAsync(nameof(MembersPage), true,

                    new Dictionary<string, object>
                    {
                        { "UserViewModel", userViewModel }
                    });
                
            }
            catch
            {
                await Shell.Current.DisplayAlert("Nice trye", $"Thats not you password", "OK");
                return;
            }









        }
        private async void RegisterAsync()
        {

            try
            {
                var userManagementViewModel = new UserManagementViewModel();


                // check if user already exists, if not add location

                await Shell.Current.GoToAsync(nameof(RegistrationPage), true,

                    new Dictionary<string, object>
                    {
                            { "UserManagementViewModel", userManagementViewModel }
                    });

            }
            catch
            {
                await Shell.Current.DisplayAlert("Account not allowed", $"Try other credentials if you dare", "OK");
                return;
            }
        }
    }
}




