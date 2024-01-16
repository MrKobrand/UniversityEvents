using Microsoft.AspNetCore.Components;
using WebBlazor.Contracts.Navigation;

namespace WebBlazor.Services.Navigation;

/// <summary>
/// Менеджер навигации по мероприятиям.
/// </summary>
public class EventNavigationManager : IEventNavigationManager
{
    private const string CATEGORY_EVENTS_PAGE = "/{0}/event-categories/{1}/events";
    private const string EVENT_PAGE = "/{0}/event-categories/{1}/events/{2}";

    private readonly NavigationManager _navigationManager;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="navigationManager">Менеджер навигации.</param>
    public EventNavigationManager(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    /// <inheritdoc/>
    public void NavigateToEventsPage(long sectionId, long categoryId)
    {
        _navigationManager.NavigateTo(string.Format(CATEGORY_EVENTS_PAGE, sectionId, categoryId));
    }

    /// <inheritdoc/>
    public void NavigateToEventPage(long sectionId, long categoryId, long eventId)
    {
        _navigationManager.NavigateTo(string.Format(EVENT_PAGE, sectionId, categoryId, eventId));
    }
}
