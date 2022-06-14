using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using TheSideChicks.View;

namespace TheSideChicks.ViewModels
{
    public partial class ShowsViewModel : BaseViewModel
    {
        ShowService showService;
        LocationService locationService;

        
        
        public ObservableCollection<Show> Shows { get; } = new();

        public bool ShowsNotFilled { get; set; }



        //IConnectivity connectivity;

        public ShowsViewModel(ShowService showService, LocationService locationservice, IGeolocation geolocation)
        {
            isShows();
            Title = "Shows Finder";
            this.locationService = locationservice;
            this.showService = showService;
            this.geolocation = geolocation;
        }

        public void isShows()
        {
          if (Shows.Count == 0)
                ShowsNotFilled = true;
          else
                ShowsNotFilled = false;

    
        }

        [ICommand]
        async Task GoToShowDetails(Show show)
        {
            /*if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                
                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                return;
            }*/

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


  
        
        

        [ICommand]
        async Task GoLoginAsync()
        {
            /*if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {

                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                return;
            }*/

            await Shell.Current.GoToAsync(nameof(LoginPage));

        }

        [ICommand]
        async Task GetAcceptedShowsAsync()
        {
            if(IsBusy)
                return;

            try 
            {

                IsBusy = true;
                var shows = await showService.GetShows();

                if (Shows.Count != 0)
                    Shows.Clear();

                foreach (var show in shows)
                    if ((show.accepted is true) & (show.date >= DateTime.Today))
                    {
                        Shows.Add(show);
                    }
                isShows();
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

        
    }
}
