using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
/// Класс, содержащий DI-расширения.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет зависимости <see cref="Application"/> проекта.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    /// <returns>
    /// Измененный контракт для коллекции сервисов,
    /// содержащий зависимости <see cref="Application"/> проекта.
    /// </returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
