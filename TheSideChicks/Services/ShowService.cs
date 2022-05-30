using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using TheSideChicks.Helpers;

namespace TheSideChicks.Services
{
    public class ShowService
    {
        HttpClient httpClient;
        static string BaseUrl = DeviceInfoHelper.BaseUrl;
        string Url = $"{BaseUrl}/Shows";

        public ShowService()
        {
            httpClient = new HttpClient();
        }

        List<Show> showList = new List<Show>();

        public async Task<List<Show>> GetShows()
        {

            var response = await httpClient.GetAsync(Url);  

            if (response.IsSuccessStatusCode)
            {
                showList = await response.Content.ReadFromJsonAsync<List<Show>>();
            }

            return showList;
        }

        public async Task<Show> GetShowById(int id)
        {
            var url = $"{Url}/{id}";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                showList = await response.Content.ReadFromJsonAsync<List<Show>>();
            }
            var show = showList.Find(s => s.id == id);
            return show;
        }

        public async Task<Show> AddShowAsync(Show show)
        {
            var response = await httpClient.PostAsJsonAsync(Url, show);

            if (response.IsSuccessStatusCode)
            {
                showList = await GetShows();
            }

            return show;
        }

        public async Task<Show> UpdateShowAsync(Show show)
        {
            var id = show.id;
            var url = $"{Url}/{id}";
            var response = await httpClient.PutAsJsonAsync(url, show);

            if (response.IsSuccessStatusCode)
            {
                showList = await GetShows();
            }

            return show;
        }

        public async Task DeleteShowAsync(Show show)
        {
            var id = show.id;
            var url = $"{Url}/{id}";
            var response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return;
            }

        }

    }
}
