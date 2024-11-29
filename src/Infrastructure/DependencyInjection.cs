using Infrastructure.Accounting.Extensions;
using Infrastructure.Data.Extensions;
using Infrastructure.Services.DuckDuckGoAI.Extensions;
using Infrastructure.Services.EventCategories.Extensions;
using Infrastructure.Services.Events.Extensions;
using Infrastructure.Services.EventSections.Extensions;
using Infrastructure.Services.Users.Extensions;
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
        services.AddDuckDuckGoAIHttpClient(configuration.GetSection("DuckDuckGoAIHttpClientOptions").Bind);

        services.TryAddAuthorization();
        services.TryAddEventSectionService();
        services.TryAddEventCategoryService();
        services.TryAddEventService();
        services.TryAddUserService();

        return services;
    }
}
