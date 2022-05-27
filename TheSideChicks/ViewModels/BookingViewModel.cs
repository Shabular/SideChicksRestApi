using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using Location = TheSideChicks.Models.Location;

namespace TheSideChicks.ViewModels
{
    public partial class BookingViewModel : BaseViewModel
    {
        ShowService showService;
        LocationService locationService;

        public ObservableCollection<Show> Show { get; } = new();
        public ObservableCollection<Location> Location { get; } = new();

        // propperty binding
        public string showName { get; set; }
        public string image { get; set; }
        public int locationId { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime date { get; set; }
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
/*
            var show = new Show
            {
                name = showName,
                Image = image,
                Date = date,
                Fee =fee,
                Accepted = false
            };*/

            /*var location = new Location
            {
                name = venueName,
                owner = locationOwnerName,
                street = street,
                number = number,
                PostalNumber = postalNumber,
                phonenumber = phoneNumber,
                email = email
            };*/
            AddShow = new Command(AddNewBooking);

            this.showService = showService;
            this.locationService = locationService;

            


        }
        private async void AddNewBooking()
        {

            
            var location = new Location
            {
                name = venueName,
                owner = locationOwnerName,
                street = street,
                number = number,
                postalnumber = postalNumber,
                phonenumber = phoneNumber,
                email = email
            };

            var show = new Show
            {
                name = showName,
                image = image,
                date = date,
                fee = fee,
                accepted = true
            };

            
            // check if location already exists, if not add location
            var addedLocation = await locationService.AddLocation(location);
            if (addedLocation is null)
            {
                addedLocation = await locationService.GetLocationByPostalNumber(location.postalnumber);
            }
            show.locationId = addedLocation.id;
            // add location id to show and add show
            var addedShow = await showService.AddShowAsync(show);
            return;

        }
    }
}

