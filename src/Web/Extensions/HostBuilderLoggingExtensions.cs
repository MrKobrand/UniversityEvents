using System;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Events;

namespace Web.Extensions;

/// <summary>
/// Класс, содержащий расширения логгирования для <see cref="HostBuilder"/>.
/// </summary>
public static class HostBuilderLoggingExtensions
{
    private static readonly string ExecutingAssembly = Assembly.GetEntryAssembly()!.GetName().Name!;

    /// <summary>
    /// Настраивает логгирование приложения.
    /// </summary>
    /// <param name="hostBuilder">Абстракция над инициализацией приложения.</param>
    /// <param name="configure">Дополнительные настройки конфигурации.</param>
    /// <returns>Тот же самый объект <see cref="IHostBuilder"/>.</returns>
    public static IHostBuilder UseConfigurableLogging(
        this IHostBuilder hostBuilder,
        Action<HostBuilderContext, LoggerConfiguration>? configure = null)
    {
        return hostBuilder.UseSerilog((hostBuilderContext, configuration) =>
        {
            configuration.MinimumLevel.Verbose();
            configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning);
            configuration.MinimumLevel.Override("System", LogEventLevel.Warning);
            configuration.MinimumLevel.Override("Grpc", LogEventLevel.Information);
            configuration.MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Information);
            configuration.MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information);

            configuration.Enrich.FromLogContext();
            configuration.Enrich.WithSpan(new SpanOptions
            {
                IncludeTags = true,
                IncludeBaggage = true
            });
            configuration.Enrich.WithProperty("app", ExecutingAssembly);

            configuration.ReadFrom.Configuration(hostBuilderContext.Configuration);

            configure?.Invoke(hostBuilderContext, configuration);
        });
    }

}
