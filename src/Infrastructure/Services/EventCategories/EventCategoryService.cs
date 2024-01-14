using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Contracts.EventCategories;
using Application.Contracts.EventCategories.Dto;
using Domain.Entities;
using Infrastructure.Services.EventCategories.Extensions;
using Infrastructure.Services.EventSections.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.EventCategories;

/// <summary>
/// Сервис для работы с категориями мероприятий.
/// </summary>
public class EventCategoryService : IEventCategoryService
{
    private readonly ILogger<EventCategoryService> _logger;
    private readonly IUniversityEventsDbContext _dbContext;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="logger">Логгер событий.</param>
    /// <param name="dbContext">Контекст базы данных.</param>
    /// 
    public EventCategoryService(ILogger<EventCategoryService> logger, IUniversityEventsDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<DetailedEventCategoryDto?> GetAsync(long id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<GetAsync>: {Id}", id);

        var eventCategory = await _dbContext.EventCategories
            .Include(x => x.Section)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return eventCategory?.ToDetailedDto();
    }

    /// <inheritdoc/>
    public async Task<List<DetailedEventCategoryDto>> GetListAsync(
        int? limit,
        string? search,
        long? sectionId,
        CancellationToken cancellationToken)
    {
        _logger.LogTrace("<GetListAsync>: {Limit}, {Search}, {SectionId}", limit, search, sectionId);

        var eventCategoriesQuery = _dbContext.EventCategories
            .Include(x => x.Section)
            .AsNoTracking();

        if (!string.IsNullOrEmpty(search))
        {
            eventCategoriesQuery = eventCategoriesQuery.Where(x => x.Name.Contains(search));
        }

        if (sectionId is not null)
        {
            eventCategoriesQuery = eventCategoriesQuery.Where(x => x.SectionId == sectionId);
        }

        var eventCategories = await eventCategoriesQuery
            .OrderBy(x => x.Order)
            .Take(limit ?? 25)
            .ToListAsync(cancellationToken);

        return eventCategories.ToDetailedDto();
    }

    /// <inheritdoc/>
    public async Task<EventCategoryDto> CreateAsync(string name, long sectionId, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<CreateAsync>: {Name}, {SectionId}", name, sectionId);

        var previousEventCategory = await _dbContext.EventCategories
            .Where(x => x.SectionId == sectionId)
            .OrderByDescending(x => x.Order)
            .FirstOrDefaultAsync(cancellationToken);

        var order = previousEventCategory is null
            ? 0
            : previousEventCategory.Order + 1;

        var eventCategory = new EventCategory
        {
            Name = name,
            Order = order,
            SectionId = sectionId
        };

        var createdEventCategory = await _dbContext.EventCategories.AddAsync(eventCategory, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Event category with name {Name} created", name);

        return createdEventCategory.Entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task<EventCategoryDto?> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<DeleteAsync>: {Id}", id);

        var eventCategory = await _dbContext.EventCategories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (eventCategory is not null)
        {
            var eventCategories = await _dbContext.EventCategories
                .Where(x => x.SectionId == eventCategory.SectionId && x.Order > eventCategory.Order)
                .ToListAsync(cancellationToken);

            foreach (var ec in eventCategories)
            {
                ec.Order--;
            }

            _dbContext.EventCategories.Remove(eventCategory);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Event category with {Id} found and deleted", id);
        }

        return eventCategory?.ToDto();
    }
}
