using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using TheSideChicks.View;

namespace TheSideChicks.ViewModels
{
    [QueryProperty("User", "User")]
    public partial class UserManagementViewModel : BaseViewModel
    {
        [ObservableProperty]
        User user;
        public ObservableCollection<User> Users { get; set; } = new();
        UserService userService;

        public string id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string instrument { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string passwordRepeat { get; set; }


        public bool isadmin { get; set; }



        public UserManagementViewModel(UserService userService)
        {
            Title = "User management";
            this.userService = userService;
            if (user == null)
                user = new User();


            var Users = new ObservableCollection<User>();

            _ = GetUsers();
        }

        public UserManagementViewModel()
        {
        }

        [ICommand]
        async Task NotImplemented()
        {
            await Shell.Current.DisplayAlert("Not implemented", $"TI am sorry to inform you that we did not implement this jet", "OK");
            await Shell.Current.GoToAsync($"{nameof(MembersPage)}");
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

        [ICommand]
        async Task GetUser(User user)
        {
            if (IsBusy)
                return;

            await Shell.Current.GoToAsync(nameof(ManageUserPage), true,
                new Dictionary<string, object>
                    {
                            { "User", user }
                });

        }

        [ICommand]
        async void UpdateUser()
        {
            if (IsBusy)
                return;


            try
            {
                var userPut = await userService.UpdateUserAsync(user);
            } 
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to change user: {ex.Message}", "OK");
            }


            await Shell.Current.GoToAsync(nameof(MembersPage));

        }

        [ICommand]
        async void AddAssAdmin()
        {
            if (IsBusy)
                return;
            if (user.isadmin)
                user.isadmin = false;
            else
                user.isadmin = true;

            try
            {
                UpdateUser();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to change user: {ex.Message}", "OK");
            }


            await Shell.Current.GoToAsync(nameof(MembersPage));
        }

        [ICommand]
        async void AddUser(User user)
        {
            if (IsBusy)
                return;

            if (user == null)
                user = new User();

            try
            {
                if (password == passwordRepeat)
                {
                    user.password = password;
                } else
                {
                    await Shell.Current.DisplayAlert("Error!", $"Your passwords did not match", "OK");
                    return;
                }

                if ((user.username != null) && (user.password != null))
                {
                    user = await userService.AddUserAsync(user);
                    // here we check if the user was created correctly if not tell the user
                    //if username or user email = taken stop
                    // if mail is not eligable stop

                    if (user.username == "Already Taken")
                    {
                        await Shell.Current.DisplayAlert("Error!", $"Unable to add user, this username: {user.username} is taken", "OK");
                        return;
                    } else if (user.email == "Already Taken")
                    {
                        await Shell.Current.DisplayAlert("Error!", $"Unable to add user, the email: {user.email}  is taken", "OK");
                        return;
                    } else if (user.email == "Not eligable")
                    {
                        await Shell.Current.DisplayAlert("Error!", $"that was no emailaddress we understand, please try something like\n {user.firstname}@hotmail.com", "OK");
                        return;
                    }

                    await Shell.Current.GoToAsync(nameof(MembersPage));
                }
                    
                else
                    await Shell.Current.DisplayAlert("Error!", $"Unable to add user, have you filled in atleast username and password?", "OK");
            } 
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"An unexpected error occurred", "OK");
            }


            return;

        }
        
        [ICommand]
        async Task DeleteUser(User user)
        {
            try
            {
                var userDel = await userService.DeleteUserAsync(user);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to change user: {ex.Message}", "OK");
            }


            await Shell.Current.GoToAsync(nameof(MembersPage));

        }


    }


    }

