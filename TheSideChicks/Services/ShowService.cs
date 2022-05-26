using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace TheSideChicks.Services
{
    public class ShowService
    {
        HttpClient httpClient;
        string Url = "https://localhost:7126/api/Shows";
        public ShowService()
        {
            httpClient = new HttpClient();
        }

        List<Show> showList = new List<Show>();

        public async Task<List<Show>> GetShows()
        {
            if (showList?.Count > 0)
                return showList;

            var url = Url;

            var response = await httpClient.GetAsync(url);  

            if (response.IsSuccessStatusCode)
            {
                showList = await response.Content.ReadFromJsonAsync<List<Show>>();
            }

            return showList;
        }

        public async Task<List<Show>> AddShowAsync(Show show)
        {

            
            var response = await httpClient.PostAsJsonAsync(Url, show);

            if (response.IsSuccessStatusCode)
            {
                showList = await response.Content.ReadFromJsonAsync<List<Show>>();
            }

            return showList;
        }

    }
}
