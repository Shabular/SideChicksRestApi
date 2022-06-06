using TheSideChicks.Controllers;
using TheSideChicks.Services;
using TheSideChicks.View;

namespace TheSideChicks;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// register mobile functions
		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IMap>(Map.Default);

		//register together
		builder.Services.AddSingleton<ShowService>();
		builder.Services.AddSingleton<LocationService>();
		builder.Services.AddSingleton<UserService>();
		builder.Services.AddSingleton<NewsService>();

		builder.Services.AddSingleton<ImageController>();

		builder.Services.AddSingleton<ShowsViewModel>();
		builder.Services.AddSingleton<BookingViewModel>();
		builder.Services.AddTransient<ShowDetailsViewModel>();
		builder.Services.AddTransient<ShowManagementViewModel>();
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<UserViewModel>();
		builder.Services.AddTransient<UserManagementViewModel>();
		builder.Services.AddTransient<LocationsViewModel>();
		builder.Services.AddTransient<NewsViewModel>();

        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<ShowDetailsPage>();
        builder.Services.AddTransient<BookUsPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<MembersPage>();
        builder.Services.AddTransient<ShowManagementPage>();
        builder.Services.AddTransient<ManageBookingPage>();
        builder.Services.AddTransient<UserManagementPage>();
        builder.Services.AddTransient<ManageUserPage>();
        builder.Services.AddTransient<RegistrationPage>();
        builder.Services.AddTransient<ShowsPage>();
        builder.Services.AddTransient<AddLocationPage>();
        builder.Services.AddTransient<AddNewsPage>();
        builder.Services.AddTransient<ShowNewsPage>();

        return builder.Build();
	}
}
