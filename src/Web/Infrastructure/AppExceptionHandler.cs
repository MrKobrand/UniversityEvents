using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Web.Infrastructure;

/// <summary>
/// Обработчик исключений запросов.
/// </summary>
public class AppExceptionHandler : IExceptionHandler
{
    private readonly bool _allowFullError;

    private readonly ILogger<AppExceptionHandler> _logger;
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="logger">Логгер событий.</param>
    /// <param name="configuration">Контракт для набора свойств конфигурации приложения "ключ-значение".</param>
    public AppExceptionHandler(ILogger<AppExceptionHandler> logger, IConfiguration configuration)
    {
        _logger = logger;
        _allowFullError = configuration.GetSection("AllowFullError").Get<bool>();

        _exceptionHandlers = new()
        {
            [typeof(ArgumentException)] = HandleArgumentException,
            [typeof(UnauthorizedException)] = HandleUnauthorizedException,
            [typeof(NotFoundException)] = HandleNotFoundException,
            [typeof(NotSupportedException)] = HandleNotSupportedException,
            [typeof(NotImplementedException)] = HandleNotImplementedException
        };
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        if (_exceptionHandlers.TryGetValue(exceptionType, out var exceptionHandler))
        {
            await exceptionHandler.Invoke(httpContext, exception);
            return true;
        }
        else
        {
            await HandleException(httpContext, exception);
            return false;
        }
    }

    private Task HandleArgumentException(HttpContext httpContext, Exception ex)
    {
        return HandleExceptionAsync(ex.Message, httpContext, ex, HttpStatusCode.BadRequest);
    }

    private Task HandleUnauthorizedException(HttpContext httpContext, Exception ex)
    {
        return HandleExceptionAsync("Unauthorized", httpContext, ex, HttpStatusCode.Unauthorized);
    }

    private Task HandleNotFoundException(HttpContext httpContext, Exception ex)
    {
        return HandleExceptionAsync("Not Found", httpContext, ex, HttpStatusCode.NotFound);
    }

    private Task HandleNotSupportedException(HttpContext httpContext, Exception ex)
    {
        return HandleExceptionAsync("Method Not Allowed", httpContext, ex, HttpStatusCode.MethodNotAllowed);
    }

    private Task HandleNotImplementedException(HttpContext httpContext, Exception ex)
    {
        return HandleExceptionAsync("Not Implemented", httpContext, ex, HttpStatusCode.NotImplemented);
    }

    private Task HandleException(HttpContext httpContext, Exception ex)
    {
        return HandleExceptionAsync("Internal Server Error", httpContext, ex, HttpStatusCode.InternalServerError);
    }

    private Task HandleExceptionAsync(string title, HttpContext context, Exception ex, HttpStatusCode code)
    {
        var message = $"{code}: {ex.Message}";
        _logger.LogError(ex, message, ex.StackTrace);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) code;

        return context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = (int) code,
            Title = title,
            Detail = _allowFullError ? ex.ToString() : null,
        });
    }
}
