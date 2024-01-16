namespace WebBlazor.Contracts.Navigation;

/// <summary>
/// Менеджер навигации по категориям мероприятий.
/// </summary>
public interface IEventCategoryNavigationManager
{
    /// <summary>
    /// Переводит на страницу категорий мероприятий.
    /// </summary>
    /// <param name="sectionId">Уникальный идентификатор раздела мероприятия.</param>
    void NavigateToCategoriesPage(long sectionId);
}
