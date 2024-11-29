using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Configuration;

/// <summary>
/// Настройки DuckDuckGo AI чата для HttpClient.
/// </summary>
public class DuckDuckGoAIHttpClientOptions
{
    /// <summary>
    /// Адрес чата.
    /// </summary>
    [Required]
    public required string BaseAddress { get; set; }

    /// <summary>
    /// Используемая модель ИИ.
    /// </summary>
    [Required]
    public required string Model { get; set; }
}