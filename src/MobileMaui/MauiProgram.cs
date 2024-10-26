using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MobileMaui.Services.EventSections.Extensions;
using MobileMaui.Services.UniversityEvents.Extensions;

namespace MobileMaui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("MobileMaui.appsettings.json");

        if (stream is null)
        {
            throw new FileNotFoundException("Unable to find embedded resource appsettings.json");
        }

        var configuration = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();

        builder.Configuration.AddConfiguration(configuration);

        builder.Services.AddEventSectionService();

        builder.Services.AddUniversityEventsHttpClient(
            configuration.GetSection("UniversityEventsHttpClientOptions").Bind);

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        //builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
