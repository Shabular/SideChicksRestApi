namespace TheSideChicks.View;

public partial class MembersPage : TabbedPage
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