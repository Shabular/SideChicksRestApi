using TheSideChicks;

namespace TheSideChicks.View;


public partial class MainPage : ContentPage
{
    public MainPage(ShowsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;


    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}