using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services.DuckDuckGoAI;

/// <summary>
/// Http-клиент для работы с DuckDuckGo AI Chat.
/// </summary>
public interface IDuckDuckGoAIHttpClient
{
    /// <summary>
    /// Получает ответ на запрос от ИИ.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Ответ от ИИ.</returns>
    Task<string> GetAnswerAsync(
        string request,
        CancellationToken cancellationToken = default);
}