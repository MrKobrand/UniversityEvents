namespace MobileMaui.Contracts.Events.Dto;

/// <summary>
/// Мероприятие.
/// </summary>
public class EventDto
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Тип.
    /// </summary>
    public EventTypeDto Type { get; set; }

    /// <summary>
    /// Дата и время проведения.
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// Продолжительность.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Место проведения.
    /// </summary>
    public required string Place { get; set; }

    /// <summary>
    /// Тема.
    /// </summary>
    public required string Subject { get; set; }

    /// <summary>
    /// Объявление.
    /// </summary>
    public string? Announcement { get; set; }

    /// <summary>
    /// Контент.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Уникальный идентификатор изображения на превью в файловом хранилище.
    /// </summary>
    public long? PreviewImageId { get; set; }

    /// <summary>
    /// Уникальный идентификатор категории мероприятия.
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// Уникальный идентификатор автора.
    /// </summary>
    public long AuthorId { get; set; }
}