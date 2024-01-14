using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Contracts.EventSections;
using Application.Contracts.EventSections.Dto;
using Domain.Entities;
using Infrastructure.Services.Events.Extensions;
using Infrastructure.Services.EventSections.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.EventSections;

/// <summary>
/// Сервис для работы с разделами мероприятий.
/// </summary>
public class EventSectionService : IEventSectionService
{
    private readonly ILogger<EventSectionService> _logger;
    private readonly IUniversityEventsDbContext _dbContext;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="logger">Логгер событий.</param>
    /// <param name="dbContext">Контекст базы данных.</param>
    public EventSectionService(ILogger<EventSectionService> logger, IUniversityEventsDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<EventSectionDto?> GetAsync(long id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<GetAsync>: {Id}", id);

        var eventSection = await _dbContext.EventSections
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return eventSection?.ToDto();
    }

    /// <inheritdoc/>
    public async Task<List<EventSectionDto>> GetListAsync(int? limit, string? search, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<GetListAsync>: {Limit}, {Search}", limit, search);

        var eventSectionsQuery = _dbContext.EventSections
            .AsNoTracking();

        if (!string.IsNullOrEmpty(search))
        {
            eventSectionsQuery = eventSectionsQuery.Where(x => x.Name.Contains(search));
        }

        var eventSections = await eventSectionsQuery
            .OrderBy(x => x.Order)
            .Take(limit ?? 25)
            .ToListAsync(cancellationToken);

        return eventSections.ToDto();
    }

    /// <inheritdoc/>
    public async Task<EventSectionDto> CreateAsync(string name, string? description, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<CreateAsync>: {Name}, {Description}", name, description);

        var previousEventSection = await _dbContext.EventSections
            .OrderByDescending(x => x.Order)
            .FirstOrDefaultAsync(cancellationToken);

        var order = previousEventSection is null
            ? 0
            : previousEventSection.Order + 1;

        var eventSection = new EventSection
        {
            Name = name,
            Description = description,
            Order = order
        };

        var createdEventSection = await _dbContext.EventSections.AddAsync(eventSection, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Event section with name {Name} created", name);

        return createdEventSection.Entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task<EventSectionDto?> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<DeleteAsync>: {Id}", id);

        var eventSection = await _dbContext.EventSections.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (eventSection is not null)
        {
            var eventSections = await _dbContext.EventSections
                .Where(x => x.Order > eventSection.Order)
                .ToListAsync(cancellationToken);

            foreach (var es in eventSections)
            {
                es.Order--;
            }

            _dbContext.EventSections.Remove(eventSection);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Event section with {Id} found and deleted", id);
        }

        return eventSection?.ToDto();
    }
}
