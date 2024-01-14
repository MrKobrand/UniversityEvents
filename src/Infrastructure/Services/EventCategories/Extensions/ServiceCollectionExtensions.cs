using Application.Contracts.EventCategories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.Services.EventCategories.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/> для добавления <see cref="IEventCategoryService"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет зависимость <see cref="IEventCategoryService"/>.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddEventCategoryService(this IServiceCollection services)
    {
        services.TryAddScoped<IEventCategoryService, EventCategoryService>();
    }
}
