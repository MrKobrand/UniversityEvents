namespace Application.Contracts.Events.Dto;

/// <summary>
/// Спикер мероприятия.
/// </summary>
public class EventSpeakerDto
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Уникальный идентификатор мероприятия.
    /// </summary>
    public long EventId { get; set; }

    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public long UserId { get; set; }
}
