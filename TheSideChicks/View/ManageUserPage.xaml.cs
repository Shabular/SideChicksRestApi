namespace TheSideChicks.View;

public partial class ManageUserPage : ContentPage
{
	public ManageUserPage(UserManagementViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}