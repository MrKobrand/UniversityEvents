using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Files;
using Application.Common.Files.Dto;
using Application.Common.Interfaces;
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

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="logger">Логгер событий.</param>
    /// <param name="dbContext">Контекст базы данных.</param>
    public FileStorage(ILogger<FileStorage> logger, IUniversityEventsDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;

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
    public async Task<ImageDto> MoveAndSaveTempFileAsync(long? id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<MoveAndSaveTempFileAsync>: {Id}", id);

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

        image.StorageType = ImageStorageType.Persistent;
        await _dbContext.SaveChangesAsync(cancellationToken);

        var tempFilePath = GetPath(ImageStorageType.Temp, image.FileName);
        var persistentFilePath = GetPath(ImageStorageType.Persistent, image.FileName);

        File.Move(tempFilePath, persistentFilePath);

        _logger.LogInformation("Image with {Id} moved to persistent storage", id);

        return image.ToDto();
    }

    /// <inheritdoc/>
    public async Task<ImageDto?> DeleteFileAsync(long? id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<DeleteFileAsync>: {Id}", id);

        var image = await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (image is not null)
        {
            _dbContext.Images.Remove(image);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var filePath = GetPath(image.StorageType, image.FileName);
            File.Delete(filePath);

            _logger.LogInformation("Image with {Id} found and deleted", id);
        }

        return image?.ToDto();
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
