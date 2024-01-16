using System;
using Application.Common.Accounting.Dto;
using Application.Contracts.Users.Dto;

namespace Application.Contracts.Users;

/// <summary>
/// Сервис для аутентификации пользователей.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Предоставляет доступ пользователю к системе.
    /// </summary>
    /// <param name="userDto">Информация о пользователе.</param>
    /// <param name="tokenLifeTime">Время жизни токена.</param>
    /// <returns>Пара JWT + RT токенов.</returns>
    TokensPairDto SignIn(AuthUserDto userDto, TimeSpan tokenLifeTime);

    /// <summary>
    /// Убирает доступ пользователя к системе.
    /// </summary>
    void SignOut();
}
