namespace Domain.Enums;

/// <summary>
/// Тип хранения изображения.
/// </summary>
public enum ImageStorageType
{
    /// <summary>
    /// Значение отсутствует.
    /// </summary>
    None = 0,

    /// <summary>
    /// Временное хранилище.
    /// </summary>
    Temp = 1,

    /// <summary>
    /// Постоянное хранилище.
    /// </summary>
    Persistent = 2
}
