using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebBlazor.Contracts.Navigation;

namespace WebBlazor.Services.Navigation.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/> для добавления навигации.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет зависимости для навигации.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddNavigationServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IUniversityEventsNavigationManager, UniversityEventsNavigationManager>();
        services.TryAddSingleton<IEventCategoryNavigationManager, EventCategoryNavigationManager>();
        services.TryAddSingleton<IEventNavigationManager, EventNavigationManager>();
        services.TryAddSingleton<IUserNavigationManager, UserNavigationManager>();
    }
}
