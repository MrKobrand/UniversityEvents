using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities;

/// <summary>
/// Раздел мероприятий.
/// </summary>
public class EventSection : BaseEntity
{
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

    /// <summary>
    /// Список категорий мероприятий.
    /// </summary>
    public required List<EventCategory> Categories { get; set; }
}
