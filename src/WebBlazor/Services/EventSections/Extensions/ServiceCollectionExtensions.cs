using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebBlazor.Contracts.EventSections;

namespace WebBlazor.Services.EventSections.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/>
/// для добавления <see cref="IEventSectionService"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет зависимость <see cref="IEventSectionService"/>.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddEventSectionService(this IServiceCollection services)
    {
        services.TryAddScoped<IEventSectionService, EventSectionService>();
    }
}
