using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebBlazor.Contracts.Users;

namespace WebBlazor.Services.Users.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/>
/// для добавления <see cref="IUserService"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет зависимость <see cref="IUserService"/>.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public static void TryAddUserService(this IServiceCollection services)
    {
        services.TryAddScoped<IUserService, UserService>();
    }
}
