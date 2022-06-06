namespace TheSideChicks.View;

public partial class UserManagementPage : ContentPage
{
	public UserManagementPage(UserManagementViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }   
}