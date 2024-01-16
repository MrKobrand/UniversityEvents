using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebBlazor.Data.Extensions;
using WebBlazor.Services.Authorization.Extensions;
using WebBlazor.Services.LocalStorage.Extensions;
using WebBlazor.Services.Navigation.Extensions;

namespace WebBlazor;

/// <summary>
/// Класс, содержащий метод <see cref="Main(string[])"/> для запуска приложения.
/// </summary>
public class Program
{
    /// <summary>
    /// Служит точкой входа в приложение.
    /// </summary>
    /// <param name="args">Параметры командной строки.</param>
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddAuthorizationCore();
        builder.Services.AddBlazoredLocalStorage();

        builder.Services.TryAddAuthenticationStateProvider();
        builder.Services.TryAddHttpRepositories();
        builder.Services.TryAddNavigationServices();
        builder.Services.TryAddAuthorizationService();
        builder.Services.TryAddUniversityEventsLocalStorageService();

        await builder.Build().RunAsync();
    }
}
