namespace WebBlazor.Contracts.Navigation;

/// <summary>
/// Менеджер навигации по мероприятиям.
/// </summary>
public interface IEventNavigationManager
{
    /// <summary>
    /// Переводит на страницу мероприятий.
    /// </summary>
    /// <param name="sectionId">Уникальный идентификатор раздела мероприятия.</param>
    /// <param name="categoryId">Уникальный идентификатор категории мероприятия.</param>
    void NavigateToEventsPage(long sectionId, long categoryId);

    /// <summary>
    /// Переводит на страницу мероприятия.
    /// </summary>
    /// <param name="sectionId">Уникальный идентификатор раздела мероприятия.</param>
    /// <param name="categoryId">Уникальный идентификатор категории мероприятия.</param>
    /// <param name="eventId">Уникальный идентификатор мероприятия.</param>
    void NavigateToEventPage(long sectionId, long categoryId, long eventId);
}