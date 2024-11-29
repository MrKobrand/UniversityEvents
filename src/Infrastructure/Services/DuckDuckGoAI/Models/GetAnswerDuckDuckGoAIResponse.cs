using System.Text.Json.Serialization;

namespace Infrastructure.Services.DuckDuckGoAI.Models;

/// <summary>
/// Модель ответа от DuckDuckGo AI Chat.
/// </summary>
public class GetAnswerDuckDuckGoAIResponse
{
    /// <summary>
    /// Сообщение.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Используемая AI-модель.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// Роль, от кого отправляется сообщение.
    /// </summary>
    [JsonPropertyName("role")]
    public string? Role { get; set; }

    /// <summary>
    /// Время создания сообщения.
    /// </summary>
    [JsonPropertyName("created")]
    public long Created { get; set; }

    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Статус сообщения.
    /// </summary>
    [JsonPropertyName("action")]
    public required string Action { get; set; }
}