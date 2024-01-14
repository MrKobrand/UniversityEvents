namespace Web.Contracts.EventSections;

/// <summary>
/// Параметры на создание раздела мероприятия.
/// </summary>
public class CreateEventSectionCommand
{
    /// <summary>
    /// Наименование раздела мероприятия.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Описание раздела мероприятия.
    /// </summary>
    public string? Description { get; set; }
}
