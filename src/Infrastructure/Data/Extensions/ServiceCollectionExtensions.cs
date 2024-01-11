using System;
using Application.Common.Interfaces;
using Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Extensions;

/// <summary>
/// Класс, содержащий расширения для <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавляет подключение с базой данных мероприятий университета.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    /// <param name="connectionString">Строка подключения с базой данных.</param>
    /// <exception cref="ArgumentNullException">
    /// Выбрасывается, если был передан <see langword="null"/> вместо строки подключения.
    /// </exception>
    public static void AddUniversityEventsDbContext(this IServiceCollection services, string? connectionString)
    {
        if (connectionString is null)
        {
            throw new ArgumentNullException(
                nameof(connectionString),
                "Connection string for university events database not found.");
        }

        services.AddScoped<ISaveChangesInterceptor, BaseEntityInterceptor>();

        services.AddDbContext<UniversityEventsDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUniversityEventsDbContext>(provider =>
            provider.GetRequiredService<UniversityEventsDbContext>());
    }
}
