using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Изображение.
/// </summary>
public class Image : BaseEntity
{
    /// <summary>
    /// Наименование изображения.
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Тип содержимого изображения.
    /// </summary>
    public required string ContentType { get; set; }

    /// <summary>
    /// Тип хранения изображения.
    /// </summary>
    public ImageStorageType StorageType { get; set; }

    /// <summary>
    /// Мероприятие.
    /// </summary>
    public virtual Event? Event { get; set; }

    /// <summary>
    /// Пользователь.
    /// </summary>
    public virtual User? User { get; set; }
}
