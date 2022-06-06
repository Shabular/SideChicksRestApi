using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Location = TheSideChicks.Models.Location;
using TheSideChicks.Helpers;

namespace TheSideChicks.Services
{
    public class LocationService
    {
        HttpClient httpClient;

        static string BaseUrl = DeviceInfoHelper.BaseUrl;
        string Url = $"{BaseUrl}/Locations";
        //string Url = "https://localhost:7126/api/Locations";
        public LocationService()
        {
            httpClient = new HttpClient();
        }

        List<Location> locationList = new List<Location>();

        public async Task<List<Location>> GetLocations()
        {
            if (locationList?.Count > 0)
                locationList = new List<Location>();

            var url = Url;

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                locationList = await response.Content.ReadFromJsonAsync<List<Location>>();
            }

            return locationList;
        }

        public async Task<bool> CheckIfLocationExists(Location location)
        {
            List<Location> existingLocations = await GetLocations();
            if (existingLocations != null)
            {
                if ((existingLocations.Find(l => l.postalNumber == location.postalNumber) != null)
                    & (existingLocations.Find(l => l.number == location.number) != null))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<Location> GetLocationById(int id)
        {
            var url = $"{Url}/{id}";

            if (locationList?.Count > 0)
                locationList = new List<Location>();


            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                locationList = await GetLocations();
         
            }
            var location = locationList?.Find(l => l.id == id);
            return location;
        }


        public async Task<Location> AddLocation(Location location)
        {
            var existingLocationList = await GetLocations();

            var locationExists = existingLocationList.Find(l => l.postalNumber == location.postalNumber);
            if (locationExists != null)
                return locationExists;

            var response = await httpClient.PostAsJsonAsync(Url, location);

            if (response.IsSuccessStatusCode)
            {
                locationList = await GetLocations();
            }
            var createdLocation = locationList.Find(l => l.postalNumber == location.postalNumber);
            return createdLocation;
        }

        async Task RemoveLocation(int id)
        {
            var response = await httpClient.DeleteAsync($"{Url}/{id}");
        }

        public async Task<Location> GetLocationByPostalNumber(string postalNumber)
        {
            var existingLocationList = new List<Location>();
            existingLocationList = await GetLocations();

            var locationExists = existingLocationList.Find(l => l.postalNumber == postalNumber);
            if (locationExists != null)
                return locationExists;
            else
                return null;
        }
    }
}
