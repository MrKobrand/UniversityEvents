using Microsoft.Extensions.Logging;
using MobileMaui.Contracts.Events;
using MobileMaui.Contracts.Events.Dto;
using MobileMaui.Services.UniversityEvents;
using MobileMaui.Services.UniversityEvents.Models.Events;

namespace MobileMaui.Services.Events;

public class EventService : IEventService
{
    private readonly ILogger<EventService> _logger;
    private readonly IUniversityEventsHttpClient _universityEventsHttpClient;

    public EventService(
        ILogger<EventService> logger,
        IUniversityEventsHttpClient universityEventsHttpClient)
    {
        _logger = logger;
        _universityEventsHttpClient = universityEventsHttpClient;
    }

    public async Task<DetailedEventDto> GetAsync(long id, CancellationToken cancellationToken = default)
    {
        _logger.LogTrace("<GetAsync>: {Id}", id);

        var @event = await _universityEventsHttpClient.GetEventAsync(id, cancellationToken);

        var deteailedEvent = await GetDetailedEventDto(@event);

        return deteailedEvent;
    }

    public async Task<List<DetailedEventDto>> GetListAsync(
        int? limit = null,
        string? search = null,
        long? categoryId = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogTrace("<GetListAsync>: {Limit}, {Search}, {CategoryId}", limit, search, categoryId);

        var request = new GetEventsListUniversityEventsRequest
        {
            Limit = limit,
            Search = search,
            CategoryId = categoryId
        };

        var eventsList = await _universityEventsHttpClient.GetEventsListAsync(
            request,
            cancellationToken);

        var events = new List<DetailedEventDto>(eventsList.Count);

        foreach (var @event in eventsList)
        {
            var eventWithLoadedImages = await GetDetailedEventDto(@event);
            events.Add(eventWithLoadedImages);
        }

        return events;
    }

    private async Task<DetailedEventDto> GetDetailedEventDto(DetailedEventUniversityEventsModel model)
    {
        byte[]? previewImageData = null;
        byte[]? authorAvatarData = null;

        if (model.PreviewImageId.HasValue)
        {
            previewImageData = await _universityEventsHttpClient.GetImageAsync(model.PreviewImageId.Value);
        }

        if (model.AuthorAvatarId.HasValue)
        {
            authorAvatarData = await _universityEventsHttpClient.GetImageAsync(model.AuthorAvatarId.Value);
        }

        var speakers = new List<EventUserDto>(model.Speakers.Count);
        var participants = new List<EventUserDto>(model.Participants.Count);

        foreach (var speaker in model.Speakers)
        {
            var speakerWithLoadedImage = await GetEventUserDto(speaker);
            speakers.Add(speakerWithLoadedImage);
        }

        foreach (var participant in model.Participants)
        {
            var participantWithLoadedImage = await GetEventUserDto(participant);
            participants.Add(participantWithLoadedImage);
        }

        return new DetailedEventDto
        {
            Id = model.Id,
            Type = Enum.TryParse<EventTypeDto>(model.Type, out var eventType)
                    ? eventType
                    : EventTypeDto.None,
            Date = model.Date,
            Duration = model.Duration,
            Place = model.Place,
            Subject = model.Subject,
            Announcement = model.Announcement,
            Content = model.Content,
            PreviewImageId = model.PreviewImageId,
            CategoryId = model.CategoryId,
            AuthorId = model.AuthorId,
            PreviewImageLink = model.PreviewImageLink,
            PreviewImageData = previewImageData,
            AuthorFirstName = model.AuthorFirstName,
            AuthorLastName = model.AuthorLastName,
            AuthorMiddleName = model.AuthorMiddleName,
            AuthorAvatarId = model.AuthorAvatarId,
            AuthorAvatarLink = model.AuthorAvatarLink,
            AuthorAvatarData = authorAvatarData,
            Speakers = speakers,
            Participants = participants
        };
    }

    private async Task<EventUserDto> GetEventUserDto(EventUserUniversityEventsModel model)
    {
        byte[]? avatarData = null;

        if (model.AvatarId.HasValue)
        {
            avatarData = await _universityEventsHttpClient.GetImageAsync(model.AvatarId.Value);
        }

        return new EventUserDto
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,
            AvatarId = model.AvatarId,
            AvatarLink = model.AvatarLink,
            AvatarData = avatarData,
        };
    }
}