
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;


namespace TheSideChicks.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;

        public bool isAdmin => Preferences.Get("isAdmin", false);
        public bool isNotAdmin => !isAdmin;
        public string userID => Preferences.Get("userId", "");
        public string usernamePref => Preferences.Get("username", "");
        public bool isLoggedIn => Preferences.Get("isLoggedIn", false);
        public bool isNotLoggedIn => !isLoggedIn;

        public string bandLogo => "sidechicks.PNG";

        [ICommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [ICommand]
        async Task NotImplemented()
        {
            await Shell.Current.DisplayAlert("Not implemented", $"TI am sorry to inform you that we did not implement this jet", "OK");
            return;
        }



    }
}
