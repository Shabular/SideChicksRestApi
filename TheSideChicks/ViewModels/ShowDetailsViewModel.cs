using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.ViewModels
{
    [QueryProperty("Show", "Show")]
    public partial class ShowDetailsViewModel : BaseViewModel
    {
        public ShowDetailsViewModel()
        {
            Title = "Lets Go!";
        }

        [ObservableProperty]
        Show show;

        [ICommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
