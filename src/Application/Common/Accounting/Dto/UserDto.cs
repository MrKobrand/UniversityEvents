using Domain.Enums;

namespace Application.Common.Accounting.Dto;

/// <summary>
/// Информация о пользователе.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Роль.
    /// </summary>
    public RoleType Role { get; set; }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Токен доступа.
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// Токен обновления JWT + Refresh Token.
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Чек-бокс "Запомнить меня".
    /// </summary>
    public bool RememberMe { get; set; }
}
