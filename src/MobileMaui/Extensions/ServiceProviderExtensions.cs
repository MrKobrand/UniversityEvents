using Microsoft.Extensions.Options;

namespace MobileMaui.Extensions;

public static class ServiceProviderExtensions
{
    public static T GetOptionsValue<T>(this IServiceProvider serviceProvider) where T : class
    {
        return serviceProvider.GetRequiredService<IOptions<T>>().Value;
    }
}