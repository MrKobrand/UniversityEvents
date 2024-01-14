using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Files.Dto;

namespace Application.Common.Files;

/// <summary>
/// Сервис для работы с файловым хранилищем.
/// </summary>
public interface IFileStorage
{
    /// <summary>
    /// Открывает изображение.
    /// </summary>
    /// <param name="id">Уникальный идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Изображение.</returns>
    Task<ImageTransferDto?> OpenAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Создает изображение в временном хранилище.
    /// </summary>
    /// <param name="content">Поток байтов, представляющих изображение.</param>
    /// <param name="fileName">Наименование файла.</param>
    /// <param name="contentType">Тип содержимого файла.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Изображение с временным типом хранилища.</returns>
    Task<ImageDto> CreateAsync(
        Stream content,
        string fileName,
        string contentType,
        CancellationToken cancellationToken);

    /// <summary>
    /// Перемещает и сохраняет изображение из временного в постоянное хранилище.
    /// </summary>
    /// <param name="id">Уникальный идентификатор изображения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Изображение с измененными путем и типом хранилища.</returns>
    Task<ImageDto?> MoveAndSaveTempFileAsync(long? id, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет изображение из файлового хранилища.
    /// </summary>
    /// <param name="id">Уникальный идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Удаленное изображение.</returns>
    Task<ImageDto?> DeleteFileAsync(long? id, CancellationToken cancellationToken);
}
