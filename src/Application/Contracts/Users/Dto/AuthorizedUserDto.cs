using Domain.Enums;

namespace Application.Contracts.Users.Dto;

/// <summary>
/// Информация об авторизованном пользователе.
/// </summary>
public class AuthorizedUserDto
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
}
