using System.Collections.Generic;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

/// <summary>
/// Пользователь.
/// </summary>
public class User : BaseEntity
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
    /// Хэшированный пароль.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Соль хэшированного пароля.
    /// </summary>
    public required string PasswordSalt { get; set; }

    /// <summary>
    /// Уникальный идентификатор аватара.
    /// </summary>
    public long? AvatarId { get; set; }

    /// <summary>
    /// Изображение аватара.
    /// </summary>
    public virtual Image? AvatarImage { get; set; }

    /// <summary>
    /// Мероприятия, в которых является автором.
    /// </summary>
    public virtual List<Event> EventsAsAuthor { get; set; } = default!;

    /// <summary>
    /// Мероприятия, в которых принимает участие как спикер.
    /// </summary>
    public virtual List<EventSpeaker> EventSpeakers { get; set; } = default!;

    /// <summary>
    /// Мероприятия, в которых принимает участие.
    /// </summary>
    public virtual List<EventParticipant> EventParticipants { get; set; } = default!;
}
