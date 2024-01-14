namespace Web.Contracts.EventCategories;

/// <summary>
/// Параметры для создания категории мероприятия.
/// </summary>
public class CreateEventCategoryCommand
{
    /// <summary>
    /// Наименование.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Уникальный идентификатор раздела мероприятий.
    /// </summary>
    public long SectionId { get; set; }
}
