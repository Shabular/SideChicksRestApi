using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using TheSideChicks.View;

namespace TheSideChicks.ViewModels
{
    public partial class UserViewModel : BaseViewModel
    {
        UserService userService;
        ShowService showService;
        LocationService locationService;


        public ObservableCollection<Show> Shows { get; } = new();
        public ObservableCollection<ShowsViewModel> showsViewModel { get; } = new();
        public string username { get; set; } = Preferences.Get("username", "Not logged in");

        public UserViewModel(UserService userService, ShowService showService, LocationService locationService)
        {
            Title = $"Welcom {username}";
            this.userService = userService;
            this.showService = showService;
            this.locationService = locationService;
        }

        [ICommand]
        async Task NotImplemented()
        {
            await Shell.Current.DisplayAlert("Not implemented", $"TI am sorry to inform you that we did not implement this jet", "OK");
            await Shell.Current.GoToAsync($"{nameof(MembersPage)}");
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

                foreach (var show in shows)
                    if ((show.accepted is false) & (show.date >= DateTime.Today))
                    {
                        showsViewModel.Shows.Add(show);
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

                    foreach (var show in shows)
                        if (show.accepted is true)
                        {
                            showsViewModel.Shows.Add(show);
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
        }
    }


