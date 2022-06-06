using TheSideChicks;
namespace TheSideChicks.View;

public partial class BookUsPage : ContentPage
{
	public BookUsPage(BookingViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}