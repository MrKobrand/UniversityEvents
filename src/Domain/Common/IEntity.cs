using System;

namespace Domain.Common;

/// <summary>
/// Сущность.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// Дата создания сущности.
    /// </summary>
    DateTimeOffset CreateDate { get; set; }

    /// <summary>
    /// Дата обновления сущности.
    /// </summary>
    DateTimeOffset UpdateDate { get; set; }
}
