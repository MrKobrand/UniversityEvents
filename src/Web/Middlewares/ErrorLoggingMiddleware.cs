using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Web.Middlewares;

/// <summary>
/// Промежуточное ПО, логирующее ошибки.
/// </summary>
public class ErrorLoggingMiddleware
{
    /// <summary>
    /// Пароль.
    /// </summary>
    private const string PASSWORD = "password";

    /// <summary>
    /// Json-паттерн тела запроса.
    /// </summary>
    private static readonly Regex _jsonPattern = new($"\"{PASSWORD}\":\\s*\"([^\"]*)\"\\s*(,|)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Паттерн строки запроса.
    /// </summary>
    private static readonly Regex _queryPattern =
        new($"{PASSWORD}=([^&]*)(&|)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<ErrorLoggingMiddleware> _logger;

    /// <summary>
    /// Следующий делегат запроса.
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// Создает экземпляр класса <see cref="ErrorLoggingMiddleware" />.
    /// </summary>
    /// <param name="next">Следующий делегат запроса.</param>
    /// <param name="logger">Логер.</param>
    public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Вызывает делегат события.
    /// </summary>
    /// <param name="context">Http-контекст.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            context.Request.EnableBuffering();
            await _next(context);
        }
        catch (Exception ex)
        {
            await LogErrorAsync(ex, context);
            throw;
        }
    }

    /// <summary>
    /// Логирует ошибки при запросе.
    /// </summary>
    /// <param name="ex">Выброшенное исключение.</param>
    /// <param name="context">Http-контекст.</param>
    private async Task LogErrorAsync(Exception ex, HttpContext context)
    {
        string body = await GetBodyAsync(context.Request);

        PathString path = context.Request.Path;
        string query = GetQuery(context.Request);
        string form = GetForm(context.Request);
        string user = GetUser(context);

        _logger.LogError(
            ex,
            "Request error. Path: {path}. Query: {query}. Form: {form}. Body: {body}. User: {user}. TraceId: {traceId}",
            path,
            query,
            form,
            body,
            user,
            context.TraceIdentifier);
    }

    /// <summary>
    /// Получает строку запроса из формы из Http-запроса.
    /// </summary>
    /// <param name="httpRequest">Http-запрос.</param>
    /// <returns>Сформированная строка запроса из формы.</returns>
    private string GetForm(HttpRequest httpRequest)
    {
        if (httpRequest.HasFormContentType)
        {
            return string.Join("&",
                httpRequest.Form.Where(x => !x.Key.ToLower().Equals(PASSWORD)).Select(x => $"{x.Key}={x.Value}"));
        }

        return string.Empty;
    }

    /// <summary>
    /// Получает строку запроса из Http-запроса.
    /// </summary>
    /// <param name="httpRequest">Http-запрос.</param>
    /// <returns>Строка запроса с удаленным паролем.</returns>
    private string GetQuery(HttpRequest httpRequest)
    {
        string query = httpRequest.QueryString.ToString();
        return _queryPattern.Replace(query, string.Empty);
    }

    /// <summary>
    /// Получает тело запроса из Http-запроса.
    /// </summary>
    /// <param name="httpRequest">Http-запрос.</param>
    /// <returns>Тело запроса с удаленным паролем.</returns>
    private async Task<string> GetBodyAsync(HttpRequest httpRequest)
    {
        httpRequest.Body.Seek(0L, SeekOrigin.Begin);

        using (StreamReader reader = new StreamReader(httpRequest.Body))
        {
            string body = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(body))
            {
                return string.Empty;
            }

            body = body.Replace('\r', '⤷').Replace('\n', '⤷');
            body = _jsonPattern.Replace(body, string.Empty);

            return body;
        }
    }

    /// <summary>
    /// Получает пользователя.
    /// </summary>
    /// <param name="context">Http-контекст.</param>
    /// <returns>Строка с именем, IP-адресом и портом пользователя.</returns>
    private string GetUser(HttpContext context)
    {
        var name = context.User.Identity is { IsAuthenticated: true }
            ? context.User.Identity.Name
            : "Anonymous";

        return $"{name} (IP: {context.Connection.RemoteIpAddress}:{context.Connection.RemotePort})";
    }
}
