using System;
using Domain.Enums;

namespace Application.Contracts.Events.Dto;

/// <summary>
/// Пример мероприятия.
/// </summary>
public class EventExampleDto
{
    /// <summary>
    /// Тип.
    /// </summary>
    public EventType Type { get; set; }

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
    public required string Announcement { get; set; }

    /// <summary>
    /// Контент.
    /// </summary>
    public required string Content { get; set; }
}