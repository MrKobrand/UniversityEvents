using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Infrastructure.Services.DuckDuckGoAI.Models;

/// <summary>
/// Модель запроса на ответ от DuckDuckGo AI Chat.
/// </summary>
public class GetAnswerDuckDuckGoAIRequest
{
    /// <summary>
    /// Используемая AI-модель.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    /// Сообщения AI-модели.
    /// </summary>
    [JsonPropertyName("messages")]
    public required IReadOnlyList<RequestMessageDuckDuckGoAIModel> Messages { get; set; }
}