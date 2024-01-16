using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebBlazor.Contracts.Authorization;

namespace WebBlazor.Services.Authorization.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/>
/// для добавления <see cref="IAuthorizationService"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет зависимость <see cref="IAuthorizationService"/>.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddAuthorizationService(this IServiceCollection services)
    {
        services.TryAddScoped<IAuthorizationService, AuthorizationService>();
    }

    /// <summary>
    /// Добавляет зависимость <see cref="AuthenticationStateProvider"/>.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddAuthenticationStateProvider(this IServiceCollection services)
    {
        services.TryAddScoped<AuthenticationStateProvider, UniversityEventsAuthStateProvider>();
    }
}