namespace Domain.Common;

/// <summary>
/// Модель значения перечисления.
/// </summary>
public class EnumValueModel
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Название поля.
    /// </summary>
    public required string Key { get; set; }
}
