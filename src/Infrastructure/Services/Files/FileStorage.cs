using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Files;
using Application.Common.Files.Dto;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Services.Files.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Files;

/// <summary>
/// Сервис для работы с файловым хранилищем.
/// </summary>
public class FileStorage : IFileStorage
{
    private const string TEMP_IMAGE_FOLDER = "./temp-images";
    private const string IMAGE_FOLDER = "./images";

    private readonly ILogger<FileStorage> _logger;
    private readonly IUniversityEventsDbContext _dbContext;
    private readonly TimeProvider _timeProvider;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="logger">Логгер событий.</param>
    /// <param name="dbContext">Контекст базы данных.</param>
    /// <param name="timeProvider">Сервис для работы со временем.</param>
    public FileStorage(
        ILogger<FileStorage> logger,
        IUniversityEventsDbContext dbContext,
        TimeProvider timeProvider)
    {
        _logger = logger;
        _dbContext = dbContext;
        _timeProvider = timeProvider;

        if (!Directory.Exists(TEMP_IMAGE_FOLDER))
        {
            Directory.CreateDirectory(TEMP_IMAGE_FOLDER);
        }

        if (!Directory.Exists(IMAGE_FOLDER))
        {
            Directory.CreateDirectory(IMAGE_FOLDER);
        }
    }

    /// <inheritdoc/>
    public async Task<ImageTransferDto?> OpenAsync(long id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<OpenAsync>: {Id}", id);

        var image = await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (image is null)
        {
            return null;
        }

        var link = GetPath(image.StorageType, image.FileName);
        var stream = File.OpenRead(link);

        return image.ToTransferDto(stream);
    }

    /// <inheritdoc/>
    public async Task<ImageDto> CreateAsync(
        Stream content,
        string fileName,
        string contentType,
        CancellationToken cancellationToken)
    {
        var path = GetPath(ImageStorageType.Temp, fileName);

        if (File.Exists(path))
        {
            var fileExtenstion = Path.GetExtension(fileName);
            var fileExtensionIndex = fileName.IndexOf(fileExtenstion);
            var dateTimeNow = _timeProvider.GetUtcNow().DateTime;

            fileName = fileName.Insert(fileExtensionIndex, dateTimeNow.ToString("yyyy_MM_dd_hh_mm_ss"));
            path = GetPath(ImageStorageType.Temp, fileName);
        }

        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            content.Seek(0, SeekOrigin.Begin);
            await content.CopyToAsync(fileStream);
        }

        var image = new Image
        {
            FileName = fileName,
            ContentType = contentType,
            StorageType = ImageStorageType.Temp
        };

        var createdImage = await _dbContext.Images.AddAsync(image, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var link = FileLinkHelper.GetLinkToFile(createdImage.Entity.Id)!;

        return createdImage.Entity.ToDto(link);
    }

    /// <inheritdoc/>
    public async Task<ImageDto?> MoveAndSaveTempFileAsync(long? id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<MoveAndSaveTempFileAsync>: {Id}", id);

        if (!id.HasValue)
        {
            return null;
        }

        var image = await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (image is null)
        {
            _logger.LogInformation("Image with id {Id} does not exist", id);
            throw new ArgumentException("Image with specified id does not exist", nameof(id));
        }

        if (image.StorageType != ImageStorageType.Temp)
        {
            _logger.LogInformation("Image with id {Id} does not have temp storage type", id);
            throw new ArgumentException("Image with specified id does not have temp storage type", nameof(image.StorageType));
        }

        var tempFilePath = GetPath(ImageStorageType.Temp, image.FileName);
        var persistentFilePath = GetPath(ImageStorageType.Persistent, image.FileName);

        File.Move(tempFilePath, persistentFilePath);

        image.StorageType = ImageStorageType.Persistent;
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Image with {Id} moved to persistent storage", id);

        var link = FileLinkHelper.GetLinkToFile(image.Id)!;

        return image.ToDto(link);
    }

    /// <inheritdoc/>
    public async Task<ImageDto?> DeleteFileAsync(long? id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<DeleteFileAsync>: {Id}", id);

        var image = await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (image is null)
        {
            return null;
        }

        _dbContext.Images.Remove(image);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var path = GetPath(image.StorageType, image.FileName);
        File.Delete(path);

        _logger.LogInformation("Image with {Id} found and deleted", id);

        var link = FileLinkHelper.GetLinkToFile(image.Id)!;

        return image.ToDto(link);
    }

    /// <summary>
    /// Получает путь к файлу.
    /// </summary>
    /// <param name="imageStorageType">Тип хранилища изображения.</param>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Путь к файлу.</returns>
    /// <exception cref="ArgumentException">Указанный тип хранилища не поддерживается.</exception>
    private string GetPath(ImageStorageType imageStorageType, string fileName) => imageStorageType switch
    {
        ImageStorageType.Temp => Path.Combine(TEMP_IMAGE_FOLDER, fileName),
        ImageStorageType.Persistent => Path.Combine(IMAGE_FOLDER, fileName),
        _ => throw new ArgumentException("Incorrect image storage type", nameof(imageStorageType))
    };
}
