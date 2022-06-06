namespace TheSideChicks.View;

public partial class ShowNewsPage : ContentPage
{
	public ShowNewsPage(NewsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}