using Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.Services.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/> для добавления <see cref="IEnumService"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет зависимость <see cref="IEnumService"/>.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddEnumService(this IServiceCollection services)
    {
        services.TryAddScoped<IEnumService, EnumService>();
    }

    /// <summary>
    /// Добавляет зависимость <see cref="IHttpContextWrapper"/>.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddHttpContextWrapper(this IServiceCollection services)
    {
        services.TryAddScoped<IHttpContextWrapper, HttpContextWrapper>();
    }
}
