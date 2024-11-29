using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceProvider"/>.
/// </summary>
public static class ServiceProviderExtensions
{
    /// <summary>
    /// Получить опции из конфигурации.
    /// </summary>
    /// <typeparam name="T">Тип опций.</typeparam>
    /// <param name="serviceProvider">Контракт для провайдера сервисов.</param>
    /// <returns>Опции.</returns>
    public static T GetOptionsValue<T>(this IServiceProvider serviceProvider)
        where T : class
    {
        return serviceProvider.GetRequiredService<IOptions<T>>().Value;
    }
}