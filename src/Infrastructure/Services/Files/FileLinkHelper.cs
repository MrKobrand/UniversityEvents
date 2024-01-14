namespace Infrastructure.Services.Files;

/// <summary>
/// Сервис для генерации ссылок для скачивания файлов.
/// </summary>
public static class FileLinkHelper
{
    private const string IMAGE_CONTROLLER_TEMPLATE = "api/images";

    /// <summary>
    /// Возвращает ссылку на скачивание файла.
    /// </summary>
    /// <param name="fileId">Уникальный идентификатор изображения.</param>
    /// <returns>Путь для скачивания файла.</returns>
    public static string? GetLinkToFile(long? fileId)
    {
        return fileId.HasValue
            ? $"{IMAGE_CONTROLLER_TEMPLATE}/{fileId}"
            : null;
    }
}
