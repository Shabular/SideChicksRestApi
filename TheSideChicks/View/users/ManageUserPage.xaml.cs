namespace TheSideChicks.View;

public partial class ManageUserPage : ContentPage
{
	public ManageUserPage(UserManagementViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}