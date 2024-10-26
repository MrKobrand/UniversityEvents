using MobileMaui.Configuration;
using MobileMaui.Extensions;

namespace MobileMaui.Services.UniversityEvents.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddUniversityEventsHttpClient(
        this IServiceCollection services,
        Action<UniversityEventsHttpClientOptions> configureOptions)
    {
        services.BindOptions(configureOptions);

        services.AddSingleton<IUniversityEventsResponseHandler, UniversityEventsResponseHandler>();

        services
            .AddHttpClient<IUniversityEventsHttpClient, UniversityEventsHttpClient>((sp, c) =>
            {
                var options = sp.GetOptionsValue<UniversityEventsHttpClientOptions>();
                c.BaseAddress = new Uri(options.BaseAddress);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });
    }
}