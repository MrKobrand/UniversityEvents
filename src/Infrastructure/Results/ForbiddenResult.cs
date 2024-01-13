using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Results;

/// <summary>
/// Объявляет контракт, представляющий результат отсутствия прав.
/// </summary>
public sealed class ForbiddenResult : ActionResult
{
    private static readonly Lazy<ForbiddenResult> _inst = new(() => new ForbiddenResult());

    /// <summary>
    /// Единственный экземпляр класса <see cref="ForbiddenResult"/>.
    /// </summary>
    public static ForbiddenResult Instance => _inst.Value;

    /// <summary>
    /// Данные ответа.
    /// </summary>
    private readonly ReadOnlyMemory<byte> _responseData;

    /// <summary>
    /// Создает экземпляр класса <see cref="ForbiddenResult" />.
    /// </summary>
    private ForbiddenResult()
    {
        ProblemDetails details = GetDetails();
        _responseData = BuildResponseData(details);
    }

    /// <inheritdoc/>
    public override void ExecuteResult(ActionContext context)
    {
        ExecuteResultAsync(context).GetAwaiter().GetResult();
    }

    /// <inheritdoc/>
    public override async Task ExecuteResultAsync(ActionContext context)
    {
        context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
        context.HttpContext.Response.ContentType = "application/json";
        await context.HttpContext.Response.BodyWriter.WriteAsync(_responseData);
        await context.HttpContext.Response.BodyWriter.CompleteAsync();
    }

    /// <summary>
    /// Сериализует данные ответа о проблеме в байты.
    /// </summary>
    /// <param name="problemDetails">Данные проблемы.</param>
    /// <returns>Байтовое представление проблемы.</returns>
    private ReadOnlyMemory<byte> BuildResponseData(ProblemDetails problemDetails)
    {
        byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(problemDetails);
        return new ReadOnlyMemory<byte>(bytes);
    }

    /// <summary>
    /// Получает данные об отсутствии прав на взаимодействие с ресурсом.
    /// </summary>
    /// <returns>Ошибка обработки запроса.</returns>
    private ProblemDetails GetDetails()
    {
        return new ProblemDetails
        {
            Type = "https://www.ietf.org/rfc/rfc7231.html#section-6.5.3",
            Title = "Доступ запрещён",
            Status = (int) HttpStatusCode.Forbidden
        };
    }
}
