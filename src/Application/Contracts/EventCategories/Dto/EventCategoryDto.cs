namespace Application.Contracts.EventCategories.Dto;

/// <summary>
/// Категория мероприятия.
/// </summary>
public class EventCategoryDto
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Порядок следования.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Уникальный идентификатор раздела мероприятий.
    /// </summary>
    public long SectionId { get; set; }
}
