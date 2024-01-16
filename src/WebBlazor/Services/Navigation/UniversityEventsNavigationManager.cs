using Microsoft.AspNetCore.Components;
using WebBlazor.Contracts.Navigation;

namespace WebBlazor.Services.Navigation;

/// <summary>
/// Менеджер навигации по ключевым страницам UniversityEvents.
/// </summary>
public class UniversityEventsNavigationManager : IUniversityEventsNavigationManager
{
    private const string HOME_PAGE = "/";
    private const string LOGIN_PAGE = "/login";

    private readonly NavigationManager _navigationManager;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="navigationManager">Менеджер навигации.</param>
    public UniversityEventsNavigationManager(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    /// <inheritdoc/>
    public void NavigateToLoginPage()
    {
        _navigationManager.NavigateTo(HOME_PAGE);
    }

    /// <inheritdoc/>
    public void NavigateToHomePage()
    {
        _navigationManager.NavigateTo(LOGIN_PAGE);
    }
}
