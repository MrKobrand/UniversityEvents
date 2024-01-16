using System;
using System.Security.Claims;
using Application.Common.Accounting.Dto;

namespace Application.Common.Accounting;

/// <summary>
/// Обработчик Jwt-токенов.
/// </summary>
public interface IJwtHandler
{
    /// <summary>
    /// Генерирует JWT токен на основе данных о пользователе. 
    /// </summary>
    /// <param name="userDto">Данные о пользователе.</param>
    /// <param name="tokenLifeTime">Новое время жизни токена.</param>
    /// <returns>JWT токен.</returns>
    string GenerateJwt(AuthUserDto userDto, TimeSpan tokenLifeTime);

    /// <summary>
    /// Генерирует токен для обновления пары JWT + RefreshToken.
    /// </summary>
    /// <returns>Refresh токен.</returns>
    string GenerateRefreshToken();

    /// <summary>
    /// Возвращает клаймы пользователя.
    /// </summary>
    /// <param name="userDto">Данные о пользователе.</param>
    /// <returns>Клаймы (пары ключ-значение) пользователя.</returns>
    ClaimsPrincipal GetPrincipal(AuthUserDto userDto);


    /// <summary>
    /// Декодирование JWT токена.
    /// </summary>
    /// <param name="userToken">JWT токен.</param>
    /// <returns>Данные о пользователе.</returns>
    AuthUserDto DecodeToken(string userToken);
}
