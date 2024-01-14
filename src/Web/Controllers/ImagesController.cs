using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Files;
using Application.Common.Files.Dto;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Infrastructure;

namespace Web.Controllers;

/// <summary>
/// Контроллер изображений.
/// </summary>
public class ImagesController : ApiControllerBase
{
    private readonly IFileStorage _fileStorage;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="fileStorage">Сервис для работы с файловым хранилищем.</param>
    public ImagesController(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    /// <summary>
    /// Открывает файл по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор изображения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Изображение.</returns>
    [HttpGet("{id:long}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(FileStreamResult), 200)]
    public async Task<IActionResult> OpenImage([FromRoute] long id, CancellationToken cancellationToken)
    {
        var file = await _fileStorage.OpenAsync(id, cancellationToken);

        return file switch
        {
            ImageTransferDto result => File(result.Stream, result.ContentType, result.FileName),
            null => NotFound(),
        };
    }

    /// <summary>
    /// Создает файл изображения.
    /// </summary>
    /// <param name="formFile">Файл-изображение.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Изображение с временным типом хранилища.</returns>
    /// <exception cref="ArgumentException">Выбрасывается, если не был передан файл.</exception>
    [HttpPost]
    [Authorization(RoleType.Administrator)]
    public async Task<ImageDto> CreateImage(IFormFile formFile, CancellationToken cancellationToken)
    {
        if (formFile.Length == 0)
        {
            throw new ArgumentException("No file provided", nameof(formFile));
        }

        using var stream = formFile.OpenReadStream();
        return await _fileStorage.CreateAsync(stream, formFile.FileName, formFile.ContentType, cancellationToken);
    }

    /// <summary>
    /// Удаляет изображение.
    /// </summary>
    /// <param name="id">Уникальный идентификатор изображения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Удаленное изображение.</returns>
    [HttpDelete("{id:long}")]
    [Authorization(RoleType.Administrator)]
    public Task<ImageDto?> DeleteImage([FromRoute] long id, CancellationToken cancellationToken)
    {
        return _fileStorage.DeleteFileAsync(id, cancellationToken);
    }
}
