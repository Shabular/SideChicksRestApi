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
    }
}
