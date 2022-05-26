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
        public ObservableCollection<Show> Shows { get; } = new();

        IConnectivity connectivity;
        IGeolocation geolocation;
        public ShowsViewModel(ShowService showService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "Shows Finder";
            this.showService = showService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;

        }

        [ICommand]
        async Task GetClosestShowAsync()
        {
            if (IsBusy || Shows.Count == 0)
                return;

            try
            {
                var location = await geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await geolocation.GetLocationAsync(
                        new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(30),
                        });
                }
                
                if (location == null)
                    return;

                var first = Shows.OrderBy(s => location.CalculateDistance(s.latitude, s.longitude, DistanceUnits.Kilometers).FirstOrDefault());
                if (first == null)
                    return;

                await Shell.Current.DisplayAlert("Clossest show", $"{first.Name} ar {first.Id}", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("not found", $"Closest show could not be found", "OK");
            }
        }

        [ICommand]
        async Task GoToShowDetails(Show show)
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                
                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                return;
            }

            if (show is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ShowDetailsPage)}", true,
                new Dictionary<string, object>
                {
                    { "Show", show }
                });
        }

        [ICommand]
        async Task GetShowsAsync()
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
                    Shows.Add(show);
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
