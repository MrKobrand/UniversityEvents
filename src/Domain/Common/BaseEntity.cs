using System;

namespace Domain.Common;

/// <summary>
/// Базовая сущность.
/// </summary>
public abstract class BaseEntity : IEntity
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Дата создания сущности.
    /// </summary>
    public DateTimeOffset CreateDate { get; set; }

    /// <summary>
    /// Дата обновления сущности.
    /// </summary>
    public DateTimeOffset UpdateDate { get; set; }
}
