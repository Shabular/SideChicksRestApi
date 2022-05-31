namespace TheSideChicks.View;

public partial class UserManagementPage : ContentPage
{
	public UserManagementPage(UserManagementViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}