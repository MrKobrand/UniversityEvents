using System;
using Domain.Enums;

namespace Application.Contracts.Events.Dto;

/// <summary>
/// Запрос на создание мероприятия.
/// </summary>
public class CreateEventRequestDto
{
    /// <summary>
    /// Тип мероприятия.
    /// </summary>
    public EventType Type { get; set; }

    /// <summary>
    /// Дата и время проведения мероприятия.
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// Продолжительность мероприятия.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Место проведения мероприятия.
    /// </summary>
    public required string Place { get; set; }

    /// <summary>
    /// Тема мероприятия.
    /// </summary>
    public required string Subject { get; set; }

    /// <summary>
    /// Объявление мероприятия.
    /// </summary>
    public string? Announcement { get; set; }

    /// <summary>
    /// Контент мероприятия.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Уникальный идентификатор изображения на превью мероприятия.
    /// </summary>
    public long? PreviewImageId { get; set; }

    /// <summary>
    /// Уникальный идентификатор категории мероприятия.
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// Уникальный идентификатор автора мероприятия.
    /// </summary>
    public long AuthorId { get; set; }
}
