using System.IO;

namespace Application.Common.Files.Dto;

/// <summary>
/// Изображение для скачивания.
/// </summary>
public class ImageTransferDto
{
    /// <summary>
    /// Содержимое изображения.
    /// </summary>
    public required Stream Stream { get; set; }

    /// <summary>
    /// Наименование изображения.
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Тип содержимого изображения.
    /// </summary>
    public required string ContentType { get; set; }
}
