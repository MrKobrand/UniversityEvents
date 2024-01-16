using Microsoft.AspNetCore.Components;
using WebBlazor.Contracts.Navigation;

namespace WebBlazor.Services.Navigation;

/// <summary>
/// Менеджер навигации по категориям мероприятий.
/// </summary>
public class EventCategoryNavigationManager : IEventCategoryNavigationManager
{
    private const string SECTION_EVENT_CATEGORIES_PAGE = "/{0}/event-categories";

    private readonly NavigationManager _navigationManager;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="navigationManager">Менеджер навигации.</param>
    public EventCategoryNavigationManager(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    /// <inheritdoc/>
    public void NavigateToCategoriesPage(long sectionId)
    {
        _navigationManager.NavigateTo(string.Format(SECTION_EVENT_CATEGORIES_PAGE, sectionId));
    }
}
