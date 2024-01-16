namespace WebBlazor.Contracts.EventCategories.Dto;

/// <summary>
/// Подробная категория мероприятия.
/// </summary>
public class DetailedEventCategoryDto : EventCategoryDto
{
    /// <summary>
    /// Наименование раздела мероприятия.
    /// </summary>
    public required string SectionName { get; set; }
}
