using TheSideChicks.View;
namespace TheSideChicks;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(ShowDetailsPage), typeof(ShowDetailsPage));
        Routing.RegisterRoute(nameof(BookUsPage), typeof(BookUsPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(MembersPage), typeof(MembersPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ShowManagementPage), typeof(ShowManagementPage));
        Routing.RegisterRoute(nameof(ManageBookingPage), typeof(ManageBookingPage));
        Routing.RegisterRoute(nameof(UserManagementPage), typeof(UserManagementPage));
        Routing.RegisterRoute(nameof(ManageUserPage), typeof(ManageUserPage));
        Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        Routing.RegisterRoute(nameof(ShowsPage), typeof(ShowsPage));
        Routing.RegisterRoute(nameof(AddLocationPage), typeof(AddLocationPage));
        Routing.RegisterRoute(nameof(AddNewsPage), typeof(AddNewsPage));
        Routing.RegisterRoute(nameof(ShowNewsPage), typeof(ShowNewsPage));
        Routing.RegisterRoute(nameof(PickLocationPage), typeof(PickLocationPage));
        Routing.RegisterRoute(nameof(BandInfoPage), typeof(BandInfoPage));
    }
}
