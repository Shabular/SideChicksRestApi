namespace TheSideChicks.View;

public partial class PickLocationPage : ContentPage
{
	public PickLocationPage(BookingViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}