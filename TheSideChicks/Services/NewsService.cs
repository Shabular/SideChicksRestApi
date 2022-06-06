using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using TheSideChicks.Helpers;

namespace TheSideChicks.Services
{
    public class NewsService
    {
        HttpClient httpClient;
        static string BaseUrl = DeviceInfoHelper.BaseUrl;
        string Url = $"{BaseUrl}/News";

        List<News> newsList = new List<News>();

        public NewsService()
        {
            httpClient = new HttpClient();
        }


        public async Task<List<News>> GetNews()
        {

            var response = await httpClient.GetAsync(Url);

            if (response.IsSuccessStatusCode)
            {
                newsList = await response.Content.ReadFromJsonAsync<List<News>>();
            }

            return newsList;
        }

        public async Task<News> GeNewsById(int id)
        {
            var url = $"{Url}/{id}";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                newsList = await response.Content.ReadFromJsonAsync<List<News>>();
            }
            var news = newsList.Find(s => s.id == id);
            return news;
        }

        public async Task<News> AddNewsAsync(News news)
        {
            var response = await httpClient.PostAsJsonAsync(Url, news);

            if (response.IsSuccessStatusCode)
            {
                newsList = await GetNews();
            }

            return news;
        }

        public async Task<News> UpdateNewsAsync(News news)
        {
            var id = news.id;
            var url = $"{Url}/{id}";
            var response = await httpClient.PutAsJsonAsync(url, news);

            if (response.IsSuccessStatusCode)
            {
                newsList = await GetNews();
            }

            return news;
        }

        public async Task DeleteNewsAsync(News news)
        {
            var id = news.id;
            var url = $"{Url}/{id}";
            var response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return;
            }

        }

    }
}
