using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;
using TheSideChicks.View;

namespace TheSideChicks.ViewModels
{
    [QueryProperty("ShowsViewModel", "ShowsViewModel")]
    public partial class ShowManagementViewModel : BaseViewModel
    {
        [ObservableProperty]
        ShowsViewModel showsViewModel;

        LocationService locationService;
        ShowService showService;
        


        public ShowManagementViewModel(LocationService locationService,ShowService showService)
        {
            Title = "Lets Go!";
            Console.Write(showsViewModel);
            this.locationService = locationService;
            this.showService = showService; 
        }

        
        


       

        [ICommand]
        async Task GoToShowManagement(Show show)
        {
            /*if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                
                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                return;
            }*/
            Console.Write(show);
            if (show is null)
            {
                await Shell.Current.DisplayAlert("Internet issue", $"Check your internet and try again", "OK");
                return;
            }


            ShowTime showtime = new ShowTime();
            showtime.show = show;
            showtime.location = await locationService.GetLocationById(show.locationId);


            await Shell.Current.GoToAsync($"{nameof(ManageBookingPage)}", true,

                new Dictionary<string, object>
                {
                    { "ShowTime", showtime }
                });
        }
        

    }
}
