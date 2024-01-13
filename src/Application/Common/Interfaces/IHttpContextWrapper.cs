using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces;

/// <summary>
/// Сервис для получения доступа к контексту http-запроса.
/// </summary>
public interface IHttpContextWrapper
{
    /// <summary>
    /// Входит в систему.
    /// </summary>
    /// <param name="cookieKey">Ключ куки.</param>
    /// <param name="cookieValue">Значение куки.</param>
    void SignIn(string cookieKey, string cookieValue);

    /// <summary>
    /// Входит в систему.
    /// </summary>
    /// <param name="cookieKey">Ключ куки.</param>
    /// <param name="cookieValue">Значение куки.</param>
    /// <param name="options">Опции куки.</param>
    void SignIn(string cookieKey, string cookieValue, CookieOptions options);

    /// <summary>
    /// Выходит из системы.
    /// </summary>
    /// <param name="cookieKey">Ключ куки.</param>
    void SignOut(string cookieKey);

    /// <summary>
    /// Получает значение с ассоциированным ключом.
    /// </summary>
    /// <param name="key">Ключ куки.</param>
    /// <param name="value">Значение куки.</param>
    /// <returns><see langword="true"/>, если ключ куки присутствует, иначе - <see langword="false"/>.</returns>
    bool TryGetValue(string key, [MaybeNullWhen(false)] out string? value);
}
