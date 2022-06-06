namespace TheSideChicks.View;

public partial class ManageBookingPage : ContentPage
{
	public ManageBookingPage(ShowDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}