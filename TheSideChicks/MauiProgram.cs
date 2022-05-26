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

		builder.Services.AddSingleton<ShowsViewModel>();
		builder.Services.AddTransient<ShowDetailsViewModel>();

        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<ShowDetailsPage>();

        return builder.Build();
	}
}
