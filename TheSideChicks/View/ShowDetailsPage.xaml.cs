namespace TheSideChicks.View;


public partial class ShowDetailsPage : ContentPage
{
	public ShowDetailsPage(ShowDetailsViewModel viewModel
		)
	{
		InitializeComponent();
		BindingContext = viewModel;	
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

}