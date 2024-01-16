using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WebBlazor.Data.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/> для добавления Http-репозиториев.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет зависимости для Http-репозиториев.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddHttpRepositories(this IServiceCollection services)
    {
        services.TryAddScoped<IHttpRepository, HttpRepository>();
        services.TryAddScoped<IAuthHttpRepository, AuthHttpRepository>();
    }
}
