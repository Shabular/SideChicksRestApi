using TheSideChicks;

namespace TheSideChicks.View;


public partial class MainPage : ContentPage
{
    public MainPage(ShowsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;


    }
}