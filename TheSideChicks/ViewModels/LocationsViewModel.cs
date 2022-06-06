﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using TheSideChicks.View;
using Location = TheSideChicks.Models.Location;

namespace TheSideChicks.ViewModels
{
    public partial class LocationsViewModel : BaseViewModel
    {
        LocationService locationService;
        UserService userService;

        [ObservableProperty]
        Location location;
        public ObservableCollection<Location> Locations { get; } = new();


        //IConnectivity connectivity;

        public LocationsViewModel(LocationService locationservice, UserService userService)
        {

            Title = "Add Location";
            this.locationService = locationservice;
            this.userService = userService; 

            if (location == null)
                location = new Location();

        }


        [ICommand]
        async Task AddLocation()
        {
            if (IsBusy)
                return;

            if (location == null)
                location = new Location();

            try
            {
                bool doesExist = await locationService.CheckIfLocationExists(location); 

                if (!doesExist)
                {
                    //here we should see if a location with same postal and number does not exist
                    User user = await userService.GetUser(Preferences.Get("userId", "Non Existent"));
                    location.userid = userID;
                    location.email = userEmail;
                    var addedLocation = locationService.AddLocation(location);
                    await Shell.Current.GoToAsync(nameof(MembersPage));
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error!", $"Unable to add location,this postal and number is already taken", "OK");

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"An unexpected error occurred", "OK");
            }

        }

    }
}