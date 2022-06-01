using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Helpers;



namespace TheSideChicks.Services
{
    public class UserService
    {
        HttpClient httpClient;


        static string BaseUrl = DeviceInfoHelper.BaseUrl;
        string Url = $"{BaseUrl}/User";
        //string Url = "https://localhost:7126/api/Locations";
        public UserService()
        {
            httpClient = new HttpClient();
        }

        List<User> userList = new List<User>();

        public async Task<List<User>> GetUsers()
        {
            if (userList?.Count > 0)
                userList = new List<User>();

            var url = Url;

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                userList = await response.Content.ReadFromJsonAsync<List<User>>();
            }

            return userList;
        }
        
        public async Task<User> UpdateUserAsync(User user)
        {
            var id = user.id;
            var urll = $"{Url}/{id}";
            var response = await httpClient.PutAsJsonAsync(urll, user);

            if (response.IsSuccessStatusCode)
            {
                userList = await GetUsers();
            }

            return user;
        }

        public async Task<User> CheckIfUserInDatabase(User user)
        {

            var response = await httpClient.GetAsync(Url);

            if (response.IsSuccessStatusCode)
            {
                userList = await response.Content.ReadFromJsonAsync<List<User>>();
            }

            
            var userAccount = userList?.Find(u => u.username == user.username);
            if (userAccount.password == user.password)
            {
                return userAccount;
            }

            return user;
        }

    }
}
