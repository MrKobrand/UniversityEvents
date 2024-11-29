using System;
using System.Net;
using System.Net.Http;

namespace Infrastructure.Services.DuckDuckGoAI.Exceptions;

/// <summary>
/// Исключение, выбрасываемое при получении ошибки с сервиса DuckDuckGo AI Chat.
/// </summary>
public class DuckDuckGoAIRequestException : HttpRequestException
{
    /// <summary>
    /// Конструктор исключения об ошибке с сервиса DuckDuckGo AI Chat.
    /// </summary>
    /// <param name="message">Сообщение об ошибке.</param>
    /// <param name="inner">Вложенное исключение.</param>
    /// <param name="statusCode">Http статус код.</param>
    public DuckDuckGoAIRequestException(string? message, Exception? inner, HttpStatusCode? statusCode)
        : base(message, inner, statusCode)
    {
    }
}