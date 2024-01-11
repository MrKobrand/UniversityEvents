namespace Application.Contracts.Events.Dto;

/// <summary>
/// Участник мероприятия.
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
    /// Отчество.
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Уникальный идентификатор аватара.
    /// </summary>
    public long? AvatarId { get; set; }

    /// <summary>
    /// Ссылка на аватар.
    /// </summary>
    public string? AvatarLink { get; set; }
}
