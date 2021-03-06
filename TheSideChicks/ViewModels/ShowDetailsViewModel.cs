using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Helpers;
using TheSideChicks.Services;
using TheSideChicks.View;
using Location = Microsoft.Maui.Devices.Sensors.Location;

namespace TheSideChicks.ViewModels
{
    [QueryProperty("ShowTime", "ShowTime")]
    public partial class ShowDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        ShowTime showTime;

        ShowService showService;
        LocationService locationService;

        public LocationHelper locationHelper;
        IMap map;

        public ShowDetailsViewModel(ShowService showService, LocationService locationService, IGeolocation geolocation, IMap map)
        {
            Title = "Lets Go!";
            this.showService = showService;
            this.locationService = locationService;
            this.geolocation = geolocation;

            if (locationHelper == null)
                locationHelper = new LocationHelper(geolocation, map);
            this.map = map;
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


            await Shell.Current.GoToAsync($"{nameof(MembersPage)}");
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

        [ICommand]
        async Task GetLocationShowsAsync()
        {

            if (IsBusy || showTime == null)
            {
                await Shell.Current.DisplayAlert("Error!", $"now show was found", "OK");
                return;

            }

            try
            {
                IsBusy = true;
                Location location = null;
                if (geolocation != null)
                {
                    location = await locationHelper.GetLocationFromShow(ShowTime.location);
                    
                } 

                if (location != null)
                {
                    await locationHelper.GetMaps(location);
                }



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get shows", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
