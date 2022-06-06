namespace TheSideChicks.View;

public partial class AddLocationPage : ContentPage
{
	public AddLocationPage(LocationsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}