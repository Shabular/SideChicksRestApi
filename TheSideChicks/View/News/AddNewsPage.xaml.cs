namespace TheSideChicks.View;

public partial class AddNewsPage : ContentPage
{
	public AddNewsPage(NewsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}