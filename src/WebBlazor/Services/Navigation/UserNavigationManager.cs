using Microsoft.AspNetCore.Components;
using WebBlazor.Contracts.Navigation;

namespace WebBlazor.Services.Navigation;

/// <summary>
/// Менеджер навигации по пользователям.
/// </summary>
public class UserNavigationManager : IUserNavigationManager
{
    private const string USER_PAGE = "/users/{0}";

    private readonly NavigationManager _navigationManager;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="navigationManager">Менеджер навигации.</param>
    public UserNavigationManager(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    /// <inheritdoc/>
    public void NavigateToUserPage(long userId)
    {
        _navigationManager.NavigateTo(string.Format(USER_PAGE, userId));
    }
}
