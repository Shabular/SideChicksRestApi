using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using TheSideChicks.View;
using Location = TheSideChicks.Models.Location;

namespace TheSideChicks.ViewModels
{

    [QueryProperty("LocationsList", "LocationsList")] 
    [QueryProperty("Location", "Location")] 

    public partial class BookingViewModel : BaseViewModel
    {
        ShowService showService;
        LocationService locationService;

        public ObservableCollection<Show> Show { get; } = new();

        [ObservableProperty]
        public List<Location> locationsList;

        [ObservableProperty]
        public Location location;

        // propperty binding
        public string showName { get; set; }
        public string image { get; set; }
        public string details{ get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime date { get; set; }
        public DateTime minDate { get; } = DateTime.Today;
        public bool accepted { get; set; }
        public string venueName { get; set; }
        public string locationOwnerName { get; set; }
        public string street { get; set; }
        public int number { get; set; }
        public string postalNumber { get; set; }
        public int phoneNumber { get; set; }
        public string email { get; set; }
        public double fee { get; set; }

        public System.Windows.Input.ICommand AddShow { get; }

        public BookingViewModel(ShowService showService, LocationService locationService)
        {
            Title = "Book us now";

            AddShow = new Command(AddNewBooking);

            this.showService = showService;
            this.locationService = locationService;

            // here we get the locations from the user
           

        }

        public BookingViewModel()
        {
        }

        private async void AddNewBooking()
        {


            var show = new Show
            {
                name = showName,
                userid = userId,
                locationId = locationId,
                image = image,
                date = date,
                details = details,
                fee = fee,
                accepted = false
            };

            // add location id to show and add show
            var addedShow = await showService.AddShowAsync(show);

            await Shell.Current.GoToAsync($"{nameof(MembersPage)}");
            
            return;

        }

        [ICommand]
        private async void GoBookUs(Location location)
        {

            Preferences.Set("locationId", location.id);
            await Shell.Current.GoToAsync($"{nameof(BookUsPage)}", true,

                new Dictionary<string, object>
                {
                    { "Location", location }
                });
        }

        [ICommand]
        async Task BackToMembersPage()
        {
            await Shell.Current.GoToAsync($"{nameof(MembersPage)}");
        }
    }
}

