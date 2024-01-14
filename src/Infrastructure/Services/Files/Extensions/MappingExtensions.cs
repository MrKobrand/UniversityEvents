using System.IO;
using Application.Common.Files.Dto;
using Domain.Entities;

namespace Infrastructure.Services.Files.Extensions;

/// <summary>
/// Класс, содержащий расширения для преобразования типа изображения <see cref="Image"/> в производные DTO.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Преобразует сущность типа <see cref="Image"/> в <see cref="ImageDto"/>.
    /// </summary>
    /// <param name="value">Изображение.</param>
    /// <param name="link">Ссылка на изображение.</param>
    /// <returns>Основная информация об изображении.</returns>
    public static ImageDto ToDto(this Image value, string link)
    {
        return new ImageDto
        {
            Id = value.Id,
            FileName = value.FileName,
            ContentType = value.ContentType,
            Link = link,
            StorageType = value.StorageType
        };
    }

    /// <summary>
    /// Преобразует сущность типа <see cref="Image"/> в <see cref="ImageTransferDto"/>.
    /// </summary>
    /// <param name="value">Изображение.</param>
    /// <param name="stream">Содержимое изображения.</param>
    /// <returns>Основная информация об изображении.</returns>
    public static ImageTransferDto ToTransferDto(this Image value, Stream stream)
    {
        return new ImageTransferDto
        {
            Stream = stream,
            FileName = value.FileName,
            ContentType = value.ContentType
        };
    }
}
