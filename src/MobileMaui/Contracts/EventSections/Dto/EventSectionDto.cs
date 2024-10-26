namespace MobileMaui.Contracts.EventSections.Dto;

/// <summary>
/// Раздел мероприятий.
/// </summary>
public class EventSectionDto
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
    /// Описание.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Порядок следования.
    /// </summary>
    public int Order { get; set; }
}