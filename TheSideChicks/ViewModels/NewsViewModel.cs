using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using TheSideChicks.View;
using Microsoft.Maui.Media;
using TheSideChicks.Controllers;

namespace TheSideChicks.ViewModels
{
    public partial class NewsViewModel : BaseViewModel
    {
        NewsService newsService;
        ImageController imageController;

        public ObservableCollection<News> NewsList { get; } = new();

        [ObservableProperty]
        public News news;



        //IConnectivity connectivity;

        public NewsViewModel(NewsService newsService, ImageController imageController)
        {
            isNews();
            Title = "News Finder";
            this.newsService = newsService;
            this.imageController = imageController;


            if (news == null)
                news = new News();
        }



        [ICommand]
        public async void TakePhoto()
        {
            var localFilePath = await imageController.TakePhoto();
            var photoString = await imageController.ToBase64Photo(localFilePath);
            news.image = photoString;
        }


        [ICommand]
        public async Task<String> GetPhoto(News news)
        {
            var imageLocation = bandLogo;
            try
            {
                imageLocation = await imageController.FromBase64Photo(news.image, "NewsItem", news.title);

            } catch
            {
                Console.Write("Image not found");
            }
            return imageLocation;
        }
        /*
                [ICommand]
                async Task GoToShowDetails(Show show)
                {
                    *//*if (connectivity.NetworkAccess != NetworkAccess.Internet)
                    {

                        await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                        return;
                    }*//*

                    if (show is null)
                        return;

                    ShowTime showtime = new ShowTime();
                    showtime.show = show;
                    showtime.location = await locationService.GetLocationById(show.locationId);


                    await Shell.Current.GoToAsync($"{nameof(ShowDetailsPage)}", true,

                        new Dictionary<string, object>
                        {
                            { "ShowTime", showtime }
                        });
                }
        */

        /*



                [ICommand]
                async Task GoLoginAsync()
                {
                    *//*if (connectivity.NetworkAccess != NetworkAccess.Internet)
                    {

                        await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                        return;
                    }*//*

                    await Shell.Current.GoToAsync(nameof(LoginPage));

                }*/

        public void isNews()
        {
            if (NewsList.Count == 0)
                NewsNotFilled = true;
            else
                NewsNotFilled = false;
        }

        [ICommand]
        async Task GetNewsAsync()
        {
            if (IsBusy)
                return;

            try
            {

                IsBusy = true;
                var newsList = await newsService.GetNews();

                if (NewsList.Count != 0)
                    NewsList.Clear();

                foreach (var news in newsList)
                {
                    news.image = await GetPhoto(news);
                    NewsList.Add(news);
                }

                isNews();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get shows: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        async Task AddNews()
        {
            if (IsBusy)
                return;

            if (news == null)
                news = new News();

            try
            {
               
                //here we should see if a location with same postal and number does not exist
                news.userid = userID;
                
                var addedNews = newsService.AddNewsAsync(news);
                await Shell.Current.GoToAsync(nameof(MembersPage));


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"An unexpected error occurred", "OK");
            }

        }

    }
}
