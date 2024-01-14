using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Contracts.Events.Dto;
using Domain.Entities;
using Infrastructure.Services.Files;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Events.Extensions;

/// <summary>
/// Класс, содержащий расширения для преобразования типа мероприятия <see cref="Event"/> в производные DTO.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Преобразует сущность типа <see cref="Event"/> в <see cref="DetailedEventDto"/>.
    /// </summary>
    /// <param name="value">Мероприятие.</param>
    /// <returns>Детализированное мероприятие.</returns>
    public static DetailedEventDto ToDetailedDto(this Event value)
    {
        return new DetailedEventDto
        {
            Id = value.Id,
            Type = value.Type,
            Date = value.Date,
            Duration = value.Duration,
            Place = value.Place,
            Subject = value.Subject,
            Announcement = value.Announcement,
            Content = value.Content,
            PreviewImageId = value.PreviewImageId,
            CategoryId = value.CategoryId,
            AuthorId = value.AuthorId,
            PreviewImageLink = FileLinkHelper.GetLinkToFile(value.PreviewImageId),
            AuthorFirstName = value.Author.FirstName,
            AuthorLastName = value.Author.LastName,
            AuthorMiddleName = value.Author.MiddleName,
            AuthorAvatarId = value.Author.AvatarId,
            AuthorAvatarLink = FileLinkHelper.GetLinkToFile(value.Author.AvatarId),
            Speakers = value.EventSpeakers.ToDto(),
            Participants = value.EventParticipants.ToDto()
        };
    }

    /// <summary>
    /// Преобразует список сущностей типа <see cref="Event"/> в <see cref="DetailedEventDto"/>.
    /// </summary>
    /// <param name="values">Мероприятия.</param>
    /// <returns>Детализированные мероприятия.</returns>
    public static List<DetailedEventDto> ToDetailedDto(this List<Event>? values)
    {
        return values is null
            ? new List<DetailedEventDto>()
            : values.Select(x => x.ToDetailedDto()).ToList();
    }

    /// <summary>
    /// Преобразует сущность типа <see cref="Event"/> в <see cref="EventDto"/>.
    /// </summary>
    /// <param name="value">Мероприятие.</param>
    /// <returns>Основная информация о мероприятии.</returns>
    public static EventDto ToDto(this Event value)
    {
        return new EventDto
        {
            Id = value.Id,
            Type = value.Type,
            Date = value.Date,
            Duration = value.Duration,
            Place = value.Place,
            Subject = value.Subject,
            Announcement = value.Announcement,
            Content = value.Content,
            PreviewImageId = value.PreviewImageId,
            CategoryId = value.CategoryId,
            AuthorId = value.AuthorId
        };
    }

    /// <summary>
    /// Преобразует список сущностей типа <see cref="EventSpeaker"/> в <see cref="EventUserDto"/>.
    /// </summary>
    /// <param name="values">Список спикеров мероприятия.</param>
    /// <returns>Список пользователей-спикеров мероприятия.</returns>
    public static List<EventUserDto> ToDto(this List<EventSpeaker> values)
    {
        return values.Select(x => x.User.ToDto()).ToList();
    }

    /// <summary>
    /// Преобразует список сущностей типа <see cref="EventParticipant"/> в <see cref="EventUserDto"/>.
    /// </summary>
    /// <param name="values">Список участников мероприятия.</param>
    /// <returns>Список пользователей-участников мероприятия.</returns>
    public static List<EventUserDto> ToDto(this List<EventParticipant> values)
    {
        return values.Select(x => x.User.ToDto()).ToList();
    }

    /// <summary>
    /// Преобразует сущность типа <see cref="User"/> в <see cref="EventUserDto"/>.
    /// </summary>
    /// <param name="value">Пользователь.</param>
    /// <returns>Краткая информация о пользователе для мероприятия.</returns>
    public static EventUserDto ToDto(this User value)
    {
        return new EventUserDto
        {
            Id = value.Id,
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
            AvatarId = value.AvatarId,
            AvatarLink = FileLinkHelper.GetLinkToFile(value.AvatarId)
        };
    }

    /// <summary>
    /// Преобразует перечисление типа <see cref="User"/> в список с пагинацией.
    /// </summary>
    /// <param name="values">Список сущностей.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список с пагинацией.</returns>
    public static async Task<PaginatedList<DetailedEventDto>> ToPaginatedListAsync(
        this IQueryable<Event> values, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var count = await values.CountAsync(cancellationToken);

        var events = await values
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedList<DetailedEventDto>(events.ToDetailedDto(), count, pageNumber, pageSize);
    }

    /// <summary>
    /// Преобразует сущность типа <see cref="EventParticipant"/> в <see cref="EventParticipantDto"/>.
    /// </summary>
    /// <param name="value">Пользователь.</param>
    /// <returns>Краткая информация о пользователе для мероприятия.</returns>
    public static EventParticipantDto ToDto(this EventParticipant value)
    {
        return new EventParticipantDto
        {
            Id = value.Id,
            EventId = value.EventId,
            UserId = value.UserId
        };
    }

    /// <summary>
    /// Преобразует сущность типа <see cref="EventSpeaker"/> в <see cref="EventSpeakerDto"/>.
    /// </summary>
    /// <param name="value">Пользователь.</param>
    /// <returns>Краткая информация о пользователе для мероприятия.</returns>
    public static EventSpeakerDto ToDto(this EventSpeaker value)
    {
        return new EventSpeakerDto
        {
            Id = value.Id,
            EventId = value.EventId,
            UserId = value.UserId
        };
    }
}
