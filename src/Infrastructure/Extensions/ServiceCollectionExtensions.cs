using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Привязать опции из конфигурации.
    /// </summary>
    /// <typeparam name="T">Тип опций.</typeparam>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    /// <param name="sectionName">Название опций.</param>
    /// <returns>Билдер опций.</returns>
    public static OptionsBuilder<T> BindOptionsFromConfiguration<T>(this IServiceCollection services, string? sectionName = null) where T : class
    {
        return services
            .AddOptions<T>()
            .BindConfiguration(sectionName ?? typeof(T).Name)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    /// <summary>
    /// Привязать опции.
    /// </summary>
    /// <typeparam name="T">Тип опций.</typeparam>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    /// <param name="configureOptions">Настройки опций.</param>
    /// <returns>Билдер опций.</returns>
    public static OptionsBuilder<T> BindOptions<T>(this IServiceCollection services, Action<T> configureOptions)
        where T : class
    {
        return services
            .AddOptions<T>()
            .Configure(configureOptions)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}
