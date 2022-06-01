using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;

namespace TheSideChicks.ViewModels
{

    public partial class UserManagementViewModel : BaseViewModel
    {

        public ObservableCollection<User> Users { get; set; } = new();
        UserService userService;

        public UserManagementViewModel(UserService userService)
        {
            Title = "User management";
            this.userService = userService;
            
            var Users = new ObservableCollection<User>();

            _ = GetUsers();
        }


        [ICommand]
        async Task GetUsers()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                List<User> users = await userService.GetUsers();

                if (Users.Count != 0)
                    Users.Clear();

                foreach (User user in users)
                    Users.Add(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get shows: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
