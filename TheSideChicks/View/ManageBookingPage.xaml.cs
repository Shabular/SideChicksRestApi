namespace TheSideChicks.View;

public partial class ManageBookingPage : ContentPage
{
	public ManageBookingPage(ShowDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}