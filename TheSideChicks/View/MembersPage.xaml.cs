namespace TheSideChicks.View;

public partial class MembersPage : ContentPage
{
	public MembersPage(UserViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}