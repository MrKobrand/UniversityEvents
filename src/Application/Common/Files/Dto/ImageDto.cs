using Domain.Enums;

namespace Application.Common.Files.Dto;

/// <summary>
/// Изображение.
/// </summary>
public class ImageDto
{
    /// <summary>
    /// Уникальный идентификатор изображения.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Наименование изображения.
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Тип содержимого изображения.
    /// </summary>
    public required string ContentType { get; set; }

    /// <summary>
    /// Ссылка на изображение.
    /// </summary>
    public required string Link { get; set; }

    /// <summary>
    /// Тип хранения изображения.
    /// </summary>
    public ImageStorageType StorageType { get; set; }
}
