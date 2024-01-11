namespace Web.Configuration;

/// <summary>
/// Настройки Swagger для проекта.
/// </summary>
public class UniversityEventsSwaggerOptions
{
    /// <summary>
    /// Титульное название.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Принудительный запуск.
    /// </summary>
    public bool? ForcedSwaggerStatus { get; set; }
}
