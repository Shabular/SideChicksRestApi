
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
        public IGeolocation geolocation;
        public bool isAdmin => Preferences.Get("isAdmin", false);
        public bool isNotAdmin => !isAdmin;
        public string userId => Preferences.Get("userId", "not found");
        public int locationId => Preferences.Get("locationId", 0);
        public string userEmail => Preferences.Get("userEmail", "");
        public string usernamePref => Preferences.Get("username", "");
        public bool isLoggedIn => Preferences.Get("isLoggedIn", false);
        public bool isNotLoggedIn => !isLoggedIn;

        public bool NewsNotFilled { get; set; }



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
