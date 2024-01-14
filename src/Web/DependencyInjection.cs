using System;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Web.Configuration;
using Web.Infrastructure;
using Web.Services;

namespace Web;

/// <summary>
/// Класс, содержащий DI-расширения.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет зависимости <see cref="Web"/> проекта.
    /// </summary>
    /// <param name="services">Контракт для коллекции сервисов.</param>
    /// <param name="configuration">Контракт для набора свойств конфигурации приложения "ключ-значение".</param>
    /// <returns>
    /// Измененный контракт для коллекции сервисов, содержащий зависимости <see cref="Web"/> проекта.
    /// </returns>
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddExceptionHandler(options =>
        {
            options.ExceptionHandlingPath = "/Error";
        });

        services
            .AddMvc(options => options.Filters.Add(SupportRestfulApi.Instance))
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();

        services.AddHealthChecks().AddDbContextCheck<UniversityEventsDbContext>();

        services.AddExceptionHandler<AppExceptionHandler>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowCors", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        OptionsBuilderExtensions.ValidateOnStart(
            services.AddOptions<UniversityEventsSwaggerOptions>().ValidateDataAnnotations());

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(ControllerSections.UNIVERSITY_EVENTS_API,
                new OpenApiInfo { Title = ControllerSections.UNIVERSITY_EVENTS_API_NAME, Version = "1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                              "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                              "Example: \"Bearer 12345abcdef\""
            });

            options.MapType<TimeSpan>(() => new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString("00:00:00")
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
                    },
                    new string[] { }
                }
            });

            options.OrderActionsBy(description =>
                $"{description.ActionDescriptor.RouteValues["controller"]}_{description.RelativePath}");

            var array = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (string filePath in array)
            {
                options.IncludeXmlComments(filePath, includeControllerXmlComments: true);
            }
        });

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtOptions:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JwtOptions:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(configuration["JwtOptions:Key"]!)),
                    ValidateIssuerSigningKey = true
                };
            });

        return services;
    }
}
