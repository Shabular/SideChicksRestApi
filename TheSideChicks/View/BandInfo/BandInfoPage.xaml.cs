namespace TheSideChicks.View;

public partial class BandInfoPage : ContentPage
{
    public BandInfoPage(BandInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }


}