using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using UraniumUI;
using Microcharts.Maui;
using AiForms.Settings;

namespace WeightTracker;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
		    .UseMauiCommunityToolkit()
			.UseUraniumUI()
			.UseUraniumUIMaterial()
			.UseMicrocharts()
			.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddSettingsViewHandler();
			})
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Font-Awesome-6-Brands.otf", "FontAwesomeBrands");
				fonts.AddFont("Font-Awesome-6-Solid.otf", "FontAwesomeSolid");
				fonts.AddFont("Font-Awesome-6-Regular.otf", "FontAwesomeRegular");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
