using Microsoft.Extensions.DependencyInjection.Extensions;
using MobileMaui.Contracts.Events;

namespace MobileMaui.Services.Events.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddEventService(this IServiceCollection services)
    {
        services.TryAddScoped<IEventService, EventService>();
    }
}