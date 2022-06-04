namespace TheSideChicks.View;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(UserManagementViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}