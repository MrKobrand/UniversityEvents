using Domain.Enums;

namespace Web.Contracts.Users;

/// <summary>
/// Параметры запроса на создание пользователя.
/// </summary>
public class CreateUserCommand
{
    /// <summary>
    /// Имя.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Роль.
    /// </summary>
    public RoleType Role { get; set; }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Пароль.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Уникальный идентификатор аватара.
    /// </summary>
    public long? AvatarId { get; set; }
}
