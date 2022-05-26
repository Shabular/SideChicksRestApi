using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Location = TheSideChicks.Models.Location;

namespace TheSideChicks.Services
{
    public class LocationService
    {
        HttpClient httpClient;
        string Url = "https://localhost:7126/api/Locations";
        public LocationService()
        {
            httpClient = new HttpClient();
        }

        List<Location> locationList = new List<Location>();

        public async Task<List<Location>> GetLocations()
        {
            if (locationList?.Count > 0)
                return locationList;

            var url = Url;

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                locationList = await response.Content.ReadFromJsonAsync<List<Location>>();
            }

            return locationList;
        }


        public async Task<List<Location>> AddLocation(Location location)
        {
            var response = await httpClient.PostAsJsonAsync(Url, location);

            if (response.IsSuccessStatusCode)
            {
                locationList = await response.Content.ReadFromJsonAsync<List<Location>>();
            }

            return locationList;
        }

        async Task RemoveLocation(int id)
        {
            var response = await httpClient.DeleteAsync($"{Url}/{id}");
        }

    }
}
