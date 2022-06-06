using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using TheSideChicks.View;
using Location = TheSideChicks.Models.Location;

namespace TheSideChicks.ViewModels
{
    public partial class UserViewModel : BaseViewModel
    {
        UserService userService;
        ShowService showService;
        LocationService locationService;
        NewsService newsService;

        public string username = Preferences.Get("usernamePref", "");
        public bool isLoggedIn = Preferences.Get("isLoggedIn", true);

        public ObservableCollection<Show> Shows { get; } = new();
        public ObservableCollection<ShowsViewModel> showsViewModel { get; } = new();
        public ObservableCollection<News> NewsList { get; } = new();

        [ObservableProperty]
        public News news;




        public UserViewModel(UserService userService, ShowService showService, LocationService locationService, NewsService newsService)
        {

            Title = $"Welcom {username}";
            this.userService = userService;
            this.showService = showService;
            this.locationService = locationService;
            this.newsService = newsService;
        }


        [ICommand]
        async Task GetPendingShowsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                ShowsViewModel showsViewModel = new(showService, locationService);
                var shows = await showService.GetShows();

                if (Shows.Count != 0)
                    Shows.Clear();

                if (Preferences.Get("isAdmin", false) == true)
                {
                    foreach (var show in shows)
                        if ((show.accepted is false) & (show.date >= DateTime.Today))
                        {
                            showsViewModel.Shows.Add(show);
                        }
                }
                else if(Preferences.Get("userId", "") != ""){
                    foreach (var show in shows)
                        if ((show.accepted is false) & (show.date >= DateTime.Today) & (show.userid == Preferences.Get("userId", "")))
                        {
                            showsViewModel.Shows.Add(show);
                        }
                }

                if (showsViewModel.Shows.Count <= 0)
                {
                    await Shell.Current.DisplayAlert("Error!", $"There are no pending shows", "OK");
                    return;
                }
                await Shell.Current.GoToAsync(nameof(ShowManagementPage), true,
               new Dictionary<string, object>
                   {
                            { "ShowsViewModel", showsViewModel }
               });
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
        async Task GoToNewsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await Shell.Current.GoToAsync(nameof(AddNewsPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get news: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        

        [ICommand]
        async Task GetAcceptedShowsAsync()
        {
            if (IsBusy)
                return;

        try
        {
            IsBusy = true;

            ShowsViewModel showsViewModel = new(showService, locationService);
            var shows = await showService.GetShows();

            if (Shows.Count != 0)
                Shows.Clear();
            if ((Preferences.Get("isAdmin", false) == true) | (Preferences.Get("userId", "") == ""))
            {

                foreach (var show in shows)
                    if (show.accepted is true)
                    {
                        showsViewModel.Shows.Add(show);
                    }
            }
            else if (Preferences.Get("userId", "") != "")
            {
                foreach (var show in shows)
                    if ((show.accepted is true) & (show.date >= DateTime.Today) & (show.userid == Preferences.Get("userId", "")))
                    {
                        showsViewModel.Shows.Add(show);
                    }
            }
            if (showsViewModel.Shows.Count <= 0)
            {
                await Shell.Current.DisplayAlert("Error!", $"There are no pending shows", "OK");
                return;
            }

             await Shell.Current.GoToAsync(nameof(ShowManagementPage), true,
            new Dictionary<string, object>
            {
                    { "ShowsViewModel", showsViewModel }
            }); 
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
        async Task GetUsersAsync()
        {
            if (IsBusy)
                return;

            
            try
            {
                IsBusy = true;

            UserService userService = new UserService();
            var users = userService.GetUsers();

            if (isNotAdmin)
            {
                UserManagementViewModel userManagementViewModel = new UserManagementViewModel();
                    
                var user = await userService.GetUser(usernamePref);
                userManagementViewModel.User = user;
                await Shell.Current.GoToAsync(nameof(ManageUserPage), true,
                new Dictionary<string, object>
                    {
                        { "UserManagementViewModel", userManagementViewModel }
                });

            } 
            else
            {
                await Shell.Current.GoToAsync(nameof(UserManagementPage));
            }
                    
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
        async Task PickLocationAsync()
        {
            /*if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {

                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                return;
            }*/
            try
            {
                List<Location> userLocations = await locationService.GetLocationsByUserId(userId);
                await Shell.Current.GoToAsync($"{nameof(PickLocationPage)}", true,

                new Dictionary<string, object>
                {
                                { "LocationsList", userLocations }
                });
            }
            catch
            {
                await Shell.Current.DisplayAlert("Error!", $"please add a location first", "OK");
                await Shell.Current.GoToAsync(nameof(AddLocationPage));
            }


        }



        [ICommand]
        async Task GoBookUsAsync()
        {
            /*if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {

                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                return;
            }*/

            await Shell.Current.GoToAsync(nameof(BookUsPage));

        }

        [ICommand]
        async Task AddLocationAsync()
        {
            /*if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {

                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                return;
            }*/

            await Shell.Current.GoToAsync(nameof(AddLocationPage));

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
                    NewsList.Add(news);

                isNews();


                await Shell.Current.GoToAsync($"{nameof(ShowNewsPage)}", true,

                new Dictionary<string, object>
                {
                    { "NewsList", NewsList }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to get news: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void isNews()
        {
            if (NewsList.Count == 0)
                NewsNotFilled = true;
            else
                NewsNotFilled = false;
        }

    }
    
}


