using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.ViewModels
{
    [QueryProperty("ShowTime", "ShowTime")]
    public partial class ShowDetailsViewModel : BaseViewModel
    {
        public ShowDetailsViewModel()
        {
            Title = "Lets Go!";
        }

        [ObservableProperty]
        ShowTime showTime;

        [ICommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
