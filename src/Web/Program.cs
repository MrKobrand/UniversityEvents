using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Web.Extensions;

namespace Web;

/// <summary>
/// Класс, содержащий метод <see cref="Main(string[])"/> для запуска приложения.
/// </summary>
public class Program
{
    /// <summary>
    /// Служит точкой входа в приложение.
    /// </summary>
    /// <param name="args">Параметры командной строки.</param>
    public static void Main(string[] args)
    {
        Console.Title = "University Events API Host";
        CreateHostBuilder(args).Build().Run();
    }

    /// <summary>
    /// Создает <see cref="IHostBuilder"/>.
    /// </summary>
    /// <param name="args">Параметры командной строки.</param>
    /// <returns>Объект типа <see cref="IHostBuilder"/>.</returns>
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((_, config) =>
            {
                if (args.Length <= 0)
                {
                    return;
                }

                config.AddJsonFile(args[0], false, false);
                config.AddEnvironmentVariables();

                Console.WriteLine($"Using configuration file '{args[0]}'.");
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStaticWebAssets();
                webBuilder.UseStartup<Startup>();
            })
            .UseConfigurableLogging();
    }
}
