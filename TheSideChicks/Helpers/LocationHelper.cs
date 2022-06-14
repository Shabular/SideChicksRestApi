using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Location = Microsoft.Maui.Devices.Sensors.Location;

namespace TheSideChicks.Helpers
{
    public partial class LocationHelper 
    {

        public IGeolocation geolocation;
        public IMap map;

        public LocationHelper(IGeolocation geolocation, IMap map)
        {
            this.geolocation = geolocation;
            this.map = map;
        }


        public async Task<Location> GetLocation(IGeolocation geolocation)
        {

            Location location = null;

            if (geolocation == null)
            {
                return location;
            }
            //var locationService = new GoogleLocationService();
            //var point = locationService.GetLatLongFromAddress(address);

            //var latitude = point.Latitude;
            //var longitude = point.Longitude;

            location = await geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });


            }
            return location;
        }


        public async Task GetMaps(Location location)
        {
            try
            {
                await map.OpenAsync(location.Latitude, location.Longitude, new MapLaunchOptions
                {
                    Name = "Test",
                    NavigationMode = NavigationMode.None
                });
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            
        }

        public async Task<Location> GetLocationFromShow(TheSideChicks.Models.Location showLocation)
        {
            Location location = null;
            try
            {
                string test = $"{showLocation.street} {showLocation.number}, {showLocation.city} {showLocation.postalNumber} ";

                var locations = await Geocoding.GetLocationsAsync(test);

                location = locations.FirstOrDefault();

                return location;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", "We could not get the location, someone messed up here", "OK");
                return location;
            }

            

            
        }
    }
}
