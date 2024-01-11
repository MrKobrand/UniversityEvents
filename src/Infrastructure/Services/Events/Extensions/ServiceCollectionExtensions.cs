using Application.Contracts.Events;
using Infrastructure.Services.Files.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.Services.Events.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/> для добавления <see cref="IEventService"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет зависимость <see cref="IEventService"/>.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddEventService(this IServiceCollection services)
    {
        services.TryAddFileStorage();
        services.TryAddScoped<IEventService, EventService>();
    }
}
