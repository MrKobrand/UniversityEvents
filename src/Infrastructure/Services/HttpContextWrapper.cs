using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

/// <summary>
/// Сервис для получения доступа к контексту http-запроса.
/// </summary>
public class HttpContextWrapper : IHttpContextWrapper
{
    private readonly IHttpContextAccessor _accessor;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="accessor">Контракт, предоставляющий доступ к контексту http-запроса.</param>
    public HttpContextWrapper(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
        _accessor = accessor;
    }

    /// <inheritdoc/>
    public void SignIn(string cookieKey, string cookieValue)
    {
        _accessor.HttpContext?.Response.Cookies.Append(cookieKey, cookieValue);
    }

    /// <inheritdoc/>
    public void SignIn(string cookieKey, string cookieValue, CookieOptions options)
    {
        _accessor.HttpContext?.Response.Cookies.Append(cookieKey, cookieValue, options);
    }

    /// <inheritdoc/>
    public void SignOut(string cookieKey)
    {
        _accessor.HttpContext?.Response.Cookies.Delete(cookieKey);
    }

    /// <inheritdoc/>
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out string? value)
    {
        value = null;

        var httpContext = _accessor.HttpContext;
        return httpContext is not null && httpContext.Request.Cookies.TryGetValue(key, out value);
    }
}
