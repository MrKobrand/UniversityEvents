using Domain.Enums;

namespace Application.Common.Accounting;

/// <summary>
/// Авторизационные данные пользователя.
/// </summary>
public interface IUserContext
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    long Id { get; }

    /// <summary>
    /// Имя.
    /// </summary>
    string FirstName { get; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// Роль.
    /// </summary>
    RoleType Role { get; }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    string Email { get; }

    /// <summary>
    /// Токен доступа.
    /// </summary>
    string AccessToken { get; }

    /// <summary>
    /// Токен обновления JWT + Refresh Token.
    /// </summary>
    string RefreshToken { get; }

    /// <summary>
    /// Чек-бокс "Запомнить меня".
    /// </summary>
    bool RememberMe { get; }
}
