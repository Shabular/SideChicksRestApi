using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSideChicks.Services;

namespace TheSideChicks.ViewModels
{
    public partial class ShowsViewModel : BaseViewModel
    {
        ShowService showService;
        public ObservableCollection<Show> Shows { get; } = new();
        public ShowsViewModel(ShowService showService)
        {
            Title = "Shows Finder";
            this.showService = showService;

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
