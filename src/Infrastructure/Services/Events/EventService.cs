using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Files;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Contracts.Events;
using Application.Contracts.Events.Dto;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Services.DuckDuckGoAI;
using Infrastructure.Services.Events.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Events;

/// <summary>
/// Сервис для работы с мероприятиями.
/// </summary>
public class EventService : IEventService
{
    private readonly ILogger<EventService> _logger;
    private readonly IUniversityEventsDbContext _dbContext;
    private readonly IFileStorage _fileStorage;
    private readonly IEnumService _enumService;
    private readonly IDuckDuckGoAIHttpClient _duckDuckGoAIHttpClient;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="logger">Логгер событий.</param>
    /// <param name="dbContext">Контекст базы данных.</param>
    /// <param name="fileStorage">Сервис для работы с файловым хранилищем.</param>
    /// <param name="enumService">Сервис для работы с перечислениями.</param>
    /// <param name="duckDuckGoAIHttpClient">Http-клиент для работы с DuckDuckGo AI Chat.</param>
    public EventService(
        ILogger<EventService> logger,
        IUniversityEventsDbContext dbContext,
        IFileStorage fileStorage,
        IEnumService enumService,
        IDuckDuckGoAIHttpClient duckDuckGoAIHttpClient)
    {
        _logger = logger;
        _dbContext = dbContext;
        _fileStorage = fileStorage;
        _enumService = enumService;
        _duckDuckGoAIHttpClient = duckDuckGoAIHttpClient;
    }

    /// <inheritdoc/>
    public async Task<DetailedEventDto?> GetAsync(long id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<GetAsync>: {Id}", id);

        var @event = await _dbContext.Events
            .Include(x => x.Author)
            .Include(x => x.EventSpeakers)
            .ThenInclude(x => x.User)
            .Include(x => x.EventParticipants)
            .ThenInclude(x => x.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return @event?.ToDetailedDto();
    }

    /// <inheritdoc/>
    public Task<string> GetHelpAsync(string request, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<GetHelpAsync>: {Request}", request);
        return _duckDuckGoAIHttpClient.GetAnswerAsync(request, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<DetailedEventDto>> GetListAsync(
        int? limit,
        string? search,
        long? categoryId,
        CancellationToken cancellationToken)
    {
        _logger.LogTrace("<GetListAsync>: {Limit}, {Search}", limit, search);

        var eventsQuery = _dbContext.Events
            .Include(x => x.Author)
            .Include(x => x.EventSpeakers)
            .ThenInclude(x => x.User)
            .Include(x => x.EventParticipants)
            .ThenInclude(x => x.User)
            .AsNoTracking();

        if (!string.IsNullOrEmpty(search))
        {
            eventsQuery = eventsQuery.Where(x => x.Subject.Contains(search));
        }

        if (categoryId.HasValue)
        {
            eventsQuery = eventsQuery.Where(x => x.CategoryId == categoryId);
        }

        var events = await eventsQuery
            .Take(limit ?? 25)
            .ToListAsync(cancellationToken);

        return events.ToDetailedDto();
    }

    /// <inheritdoc/>
    public Task<PaginatedList<DetailedEventDto>> GetPageAsync(
        int? page,
        int? limit,
        string? search,
        long? categoryId,
        CancellationToken cancellationToken)
    {
        _logger.LogTrace("<GetPageAsync>: {Page}, {Limit}, {Search}", page, limit, search);

        var eventsQuery = _dbContext.Events
            .Include(x => x.Author)
            .Include(x => x.EventSpeakers)
            .ThenInclude(x => x.User)
            .Include(x => x.EventParticipants)
            .ThenInclude(x => x.User)
            .AsNoTracking();

        if (!string.IsNullOrEmpty(search))
        {
            eventsQuery = eventsQuery.Where(x => x.Subject.Contains(search));
        }

        if (categoryId.HasValue)
        {
            eventsQuery = eventsQuery.Where(x => x.CategoryId == categoryId);
        }

        return eventsQuery.ToPaginatedListAsync(page ?? 1, limit ?? 25, cancellationToken);
    }

    /// <inheritdoc/>
    public List<EnumValueModel> GetTypes()
    {
        _logger.LogTrace("<GetTypes>");

        return _enumService.GetEnumInfo<EventType>();
    }

    /// <inheritdoc/>
    public async Task<EventDto> CreateAsync(CreateEventRequestDto request, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<CreateAsync>: {@Request}", request);

        using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        await _fileStorage.MoveAndSaveTempFileAsync(request.PreviewImageId, cancellationToken);

        var @event = new Event
        {
            Type = request.Type,
            Date = request.Date,
            Duration = request.Duration,
            Place = request.Place,
            Subject = request.Subject,
            Announcement = request.Announcement,
            Content = request.Content,
            PreviewImageId = request.PreviewImageId,
            CategoryId = request.CategoryId,
            AuthorId = request.AuthorId
        };

        var createdEvent = await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        _logger.LogInformation("Event with subject {Subject} created", request.Subject);

        return createdEvent.Entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task<EventDto> UpdateAsync(UpdateEventRequestDto request, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<UpdateAsync>: {@Request}", request);

        using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        var @event = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (@event is null)
        {
            _logger.LogInformation("Event with {Id} not found", request.Id);
            throw new ArgumentException("Event with specified id does not exist", nameof(request.Id));
        }

        if (@event.PreviewImageId != request.PreviewImageId)
        {
            await _fileStorage.MoveAndSaveTempFileAsync(request.PreviewImageId, cancellationToken);
            await _fileStorage.DeleteFileAsync(@event.PreviewImageId, cancellationToken);
            _logger.LogInformation("Preview image updated");
        }

        @event.Type = request.Type;
        @event.Date = request.Date;
        @event.Duration = request.Duration;
        @event.Place = request.Place;
        @event.Subject = request.Subject;
        @event.Announcement = request.Announcement;
        @event.Content = request.Content;
        @event.PreviewImageId = request.PreviewImageId;
        @event.CategoryId = request.CategoryId;
        @event.AuthorId = request.AuthorId;

        await _dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        _logger.LogInformation("Event with {Id} found and updated", request.Id);

        return @event.ToDto();
    }

    /// <inheritdoc/>
    public async Task<EventDto?> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<DeleteAsync>: {Id}", id);

        var @event = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (@event is not null)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            await _fileStorage.DeleteFileAsync(@event.PreviewImageId, cancellationToken);
            _dbContext.Events.Remove(@event);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Event with {Id} found and deleted", id);
        }

        return @event?.ToDto();
    }

    /// <inheritdoc/>
    public async Task<EventParticipantDto> AssignParticipantAsync(long id, long participantId, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<AddParticipantAsync>: {Id}, {ParticipantId}", id, participantId);

        var eventParticipant = new EventParticipant
        {
            EventId = id,
            UserId = participantId
        };

        var createdEventParticipant = await _dbContext.EventParticipants
            .AddAsync(eventParticipant, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "User with id {ParticipantId} was assigned to the event with id {Id} as a participant",
            participantId,
            id);

        return createdEventParticipant.Entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task<EventParticipantDto?> RemoveParticipantAsync(long id, long participantId, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<RemoveParticipantAsync>: {Id}, {ParticipantId}", id, participantId);

        var eventParticipant = await _dbContext.EventParticipants
            .FirstOrDefaultAsync(x => x.EventId == id && x.UserId == participantId, cancellationToken);

        if (eventParticipant is not null)
        {
            _dbContext.EventParticipants.Remove(eventParticipant);
            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User with id {ParticipantId} was removed from the event with id {Id} as a participant",
                participantId,
                id);
        }

        return eventParticipant?.ToDto();
    }

    /// <inheritdoc/>
    public async Task<EventSpeakerDto> AssignSpeakerAsync(long id, long speakerId, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<AddSpeakerAsync>: {Id}, {SpeakerId}", id, speakerId);

        var eventSpeaker = new EventSpeaker
        {
            EventId = id,
            UserId = speakerId
        };

        var createdEventSpeaker = await _dbContext.EventSpeakers
            .AddAsync(eventSpeaker, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "User with id {SpeakerId} was assigned to the event with id {Id} as a speaker",
            speakerId,
            id);

        return createdEventSpeaker.Entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task<EventSpeakerDto?> RemoveSpeakerAsync(long id, long speakerId, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<RemoveSpeakerAsync>: {Id}, {SpeakerId}", id, speakerId);

        var eventSpeaker = await _dbContext.EventSpeakers
            .FirstOrDefaultAsync(x => x.EventId == id && x.UserId == speakerId, cancellationToken);

        if (eventSpeaker is not null)
        {
            _dbContext.EventSpeakers.Remove(eventSpeaker);
            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User with id {SpeakerId} was removed from the event with id {Id} as a speaker",
                speakerId,
                id);
        }

        return eventSpeaker?.ToDto();
    }
}
