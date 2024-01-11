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
    /// <returns>Основная информация об изображении.</returns>
    public static ImageDto ToDto(this Image value)
    {
        return new ImageDto
        {
            Id = value.Id,
            FileName = value.FileName,
            Link = value.Link,
            StorageType = value.StorageType
        };
    }
}
