using Microsoft.Extensions.DependencyInjection.Extensions;
using MobileMaui.Contracts.EventSections;

namespace MobileMaui.Services.EventSections.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddEventSectionService(this IServiceCollection services)
    {
        services.TryAddScoped<IEventSectionService, EventSectionService>();
    }
}