using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;
using Web.Configuration;
using Web.Middlewares;

namespace Web;

/// <summary>
/// Класс, конфигурирующий запуск приложения.
/// </summary>
public class Startup
{
    private IConfiguration Configuration { get; }

    private IHostEnvironment Environment { get; }

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="configuration">Контракт для набора свойств конфигурации приложения "ключ-значение".</param>
    /// <param name="environment">Среда запуска приложения.</param>
    public Startup(IConfiguration configuration, IHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    /// <summary>
    /// Конфигурирует сервисы приложения.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddInfrastructureServices(Configuration);
        services.AddWebServices(Configuration);
    }

    /// <summary>
    /// Конфигурирует среду приложения.
    /// </summary>
    /// <param name="app">Контракт для конфигурирования запросов приложения.</param>
    /// <param name="env">Контракт, предоставляющий информацию о среде, в которой запущено приложение.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseAuthentication();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHealthChecks("/health");

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint(
                $"/swagger/{ControllerSections.UNIVERSITY_EVENTS_API}/swagger.json",
                ControllerSections.UNIVERSITY_EVENTS_API_NAME);

            c.DocExpansion(DocExpansion.List);
        });

        app.UseExceptionHandler(opt => { });
        app.UseMiddleware<ErrorLoggingMiddleware>();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}
