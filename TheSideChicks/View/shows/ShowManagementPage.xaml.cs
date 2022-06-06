namespace TheSideChicks.View;

public partial class ShowManagementPage : ContentPage
{
	public ShowManagementPage(ShowManagementViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;


    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}