using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebBlazor.Contracts.LocalStorage;

namespace WebBlazor.Services.LocalStorage.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/>
/// для добавления <see cref="IUniversityEventsLocalStorageService"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет зависимость <see cref="IUniversityEventsLocalStorageService"/>.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddUniversityEventsLocalStorageService(this IServiceCollection services)
    {
        services.TryAddScoped<IUniversityEventsLocalStorageService, UniversityEventsLocalStorageService>();
    }
}
