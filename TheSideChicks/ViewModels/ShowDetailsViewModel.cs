using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using TheSideChicks.View;

namespace TheSideChicks.ViewModels
{
    [QueryProperty("ShowTime", "ShowTime")]
    public partial class ShowDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        ShowTime showTime;

        ShowService showService;
        LocationService locationService;

        public ShowDetailsViewModel(ShowService showService, LocationService locationService)
        {
            Title = "Lets Go!";
            this.showService = showService;
            this.locationService = locationService;
        }



        [ICommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }


        [ICommand]
        async Task AcceptBooking(Show show)
        {
           if (Preferences.Get("isAdmin", false) == false)
            {
                await Shell.Current.DisplayAlert("Realy ?", $"You are not allowed to accept the show on our behalf", "OK");
                return;
            }


            Console.Write(show);
            if (show is null)
            {
                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                return;
            }
            show.accepted = true;
            try
            {
                var result = await showService.UpdateShowAsync(show);
            }
            catch
            {
                await Shell.Current.DisplayAlert("Could not find show in DB", $"{show}", "OK");
            }


            ShowTime showtime = new ShowTime();
            showtime.show = show;
            showtime.location = await locationService.GetLocationById(show.locationId);
            showtime.location.adress = $"{showtime.location.street} {showtime.location.number}";
            showtime.location.contactInfo = $": email: {showtime.location.email} | phone number: {showtime.location.number}";


            await Shell.Current.GoToAsync($"{nameof(ManageBookingPage)}", true,

                new Dictionary<string, object>
                {
                    { "ShowTime", showtime }
                });
        }
        
        [ICommand]
        async Task DeleteBooking(Show show)
        {
            /*if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                
                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                return;
            }*/
            Console.Write(show);
            if (show is null)
            {
                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                return;
            }
            show.accepted = true;
            try
            {
                await showService.DeleteShowAsync(show);
            }
            catch
            {
                await Shell.Current.DisplayAlert("Could not find show in DB", $"{show}", "OK");
            }


            await Shell.Current.GoToAsync($"{nameof(MembersPage)}");
        }
        
        [ICommand]
        async Task BackToMembersPage()
        {
            await Shell.Current.GoToAsync($"{nameof(MembersPage)}");
        }

    }
}
