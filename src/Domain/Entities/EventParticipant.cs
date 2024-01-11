using Domain.Common;

namespace Domain.Entities;
/// <summary>
/// Связь многие-ко-кногим между мероприятием и пользователем в качестве участника.
/// </summary>
public class EventParticipant : BaseEntity
{
    /// <summary>
    /// Уникальный идентификатор мероприятия.
    /// </summary>
    public long EventId { get; set; }

    /// <summary>
    /// Мероприятие.
    /// </summary>
    public virtual Event Event { get; set; } = default!;

    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Пользователь.
    /// </summary>
    public virtual User User { get; set; } = default!;
}
