using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities;

/// <summary>
/// Категория мероприятий.
/// </summary>
public class EventCategory : BaseEntity
{
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

    /// <summary>
    /// Раздел мероприятия.
    /// </summary>
    public required EventSection Section { get; set; }

    /// <summary>
    /// Мероприятия.
    /// </summary>
    public required List<Event> Events { get; set; }
}
