using Microsoft.Extensions.DependencyInjection.Extensions;
using MobileMaui.Contracts.EventCategories;

namespace MobileMaui.Services.EventCategories.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddEventCategoryService(this IServiceCollection services)
    {
        services.TryAddScoped<IEventCategoryService, EventCategoryService>();
    }
}