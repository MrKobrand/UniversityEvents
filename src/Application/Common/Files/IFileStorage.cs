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
    /// Перемещает и сохраняет изображение из временного в постоянное хранилище.
    /// </summary>
    /// <param name="id">Уникальный идентификатор изображения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Изображение с измененными путем и типом хранилища.</returns>
    Task<ImageDto> MoveAndSaveTempFileAsync(long? id, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет изображение из файлового хранилища.
    /// </summary>
    /// <param name="id">Уникальный идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Удаленное изображение.</returns>
    Task<ImageDto?> DeleteFileAsync(long? id, CancellationToken cancellationToken);
}
