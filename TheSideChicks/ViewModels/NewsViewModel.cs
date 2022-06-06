using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using TheSideChicks.View;
using Microsoft.Maui.Media;

namespace TheSideChicks.ViewModels
{
    public partial class NewsViewModel : BaseViewModel
    {
        NewsService newsService;

        public ObservableCollection<News> NewsList { get; } = new();

        [ObservableProperty]
        public News news;
        public bool NewsNotFilled { get; set; }


        //IConnectivity connectivity;

        public NewsViewModel(NewsService newsService)
        {
            isNews();
            Title = "News Finder";
            this.newsService = newsService;

            if (news == null)
                news = new News();


        }

        public void isNews()
        {
            if (NewsList.Count == 0)
                NewsNotFilled = true;
            else
                NewsNotFilled = false;
        }

        [ICommand]
        public async void TakePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                }
            }
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
                    NewsList.Add(news);
                    
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
                news.image = "test";
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
