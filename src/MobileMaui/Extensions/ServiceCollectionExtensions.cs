using Microsoft.Extensions.Options;

namespace MobileMaui.Extensions;

public static class ServiceCollectionExtensions
{
    public static OptionsBuilder<T> BindOptions<T>(this IServiceCollection services, Action<T> configureOptions)
        where T : class
    {
        return OptionsServiceCollectionExtensions.AddOptions<T>(services).Configure(configureOptions);
    }
}