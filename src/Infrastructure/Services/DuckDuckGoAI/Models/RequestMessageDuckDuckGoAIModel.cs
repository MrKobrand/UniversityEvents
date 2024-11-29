using System.Text.Json.Serialization;

namespace Infrastructure.Services.DuckDuckGoAI.Models;

/// <summary>
/// Модель сообщения для DuckDuckGo AI Chat.
/// </summary>
public class RequestMessageDuckDuckGoAIModel
{
    /// <summary>
    /// Роль, от кого отправляется сообщение.
    /// </summary>
    [JsonPropertyName("role")]
    public required string Role { get; set; }

    /// <summary>
    /// Содержимое сообщения.
    /// </summary>
    [JsonPropertyName("content")]
    public required string Content { get; set; }
}