using Application.Common.Accounting;
using Infrastructure.Configuration;
using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infrastructure.Accounting.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/> для добавления зависимостей по авторизации.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет зависимости для авторизации.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddAuthorization(this IServiceCollection services)
    {
        services.BindOptionsFromConfiguration<JwtOptions>();

        services.TryAddScoped<UserContextAccessor>();
        services.TryAddScoped<IUserContext>(x => x.GetRequiredService<UserContextAccessor>());
        services.TryAddScoped<IUserContextAccessor>(x => x.GetRequiredService<UserContextAccessor>());

        services.TryAddScoped<IJwtHandler, JwtHandler>();
    }
}
