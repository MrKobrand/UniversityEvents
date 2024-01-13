using Infrastructure.Accounting.Extensions;
using Infrastructure.Data.Extensions;
using Infrastructure.Services.Events.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

/// <summary>
/// Класс, содержащий DI-расширения.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет зависимости <see cref="Infrastructure"/> проекта.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    /// <param name="configuration">Контракт для набора свойств конфигурации приложения "ключ-значение".</param>
    /// <returns>
    /// Измененный контракт для коллекции сервисов, содержащий зависимости <see cref="Infrastructure"/> проекта.
    /// </returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddUniversityEventsDbContext(configuration.GetConnectionString("UniversityEventsDbContext"));

        services.TryAddAuthorization();
        services.TryAddEventService();

        return services;
    }
}
