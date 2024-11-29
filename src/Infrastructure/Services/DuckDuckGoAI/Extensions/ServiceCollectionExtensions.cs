using System;
using Infrastructure.Configuration;
using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.DuckDuckGoAI.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/> для добавления <see cref="IDuckDuckGoAIHttpClient"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет зависимость <see cref="IDuckDuckGoAIHttpClient"/>.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    /// <param name="configureOptions">Опции для настройки HttpClient.</param>
    public static void AddDuckDuckGoAIHttpClient(
        this IServiceCollection services,
        Action<DuckDuckGoAIHttpClientOptions> configureOptions)
    {
        services.BindOptions(configureOptions);

        services
            .AddHttpClient<IDuckDuckGoAIHttpClient, DuckDuckGoAIHttpClient>((sp, c) =>
            {
                var options = sp.GetOptionsValue<DuckDuckGoAIHttpClientOptions>();
                c.BaseAddress = new Uri(options.BaseAddress);
            });
    }
}