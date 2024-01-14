using System.Collections.Generic;
using System.Linq;
using Application.Contracts.EventCategories.Dto;
using Domain.Entities;

namespace Infrastructure.Services.EventCategories.Extensions;

/// <summary>
/// Класс, содержащий расширения для преобразования типа
/// раздела мероприятия <see cref="EventCategory"/> в производные DTO.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Преобразует сущность типа <see cref="EventCategory"/> в <see cref="DetailedEventCategoryDto"/>.
    /// </summary>
    /// <param name="value">Категория мероприятия.</param>
    /// <returns>Детализированная категория мероприятия.</returns>
    public static DetailedEventCategoryDto ToDetailedDto(this EventCategory value)
    {
        return new DetailedEventCategoryDto
        {
            Id = value.Id,
            Name = value.Name,
            Order = value.Order,
            SectionId = value.SectionId,
            SectionName = value.Section.Name
        };
    }

    /// <summary>
    /// Преобразует список сущностей типа <see cref="EventCategory"/> в <see cref="DetailedEventCategoryDto"/>.
    /// </summary>
    /// <param name="values">Категории мероприятий.</param>
    /// <returns>Детализированные категории мероприятий.</returns>
    public static List<DetailedEventCategoryDto> ToDetailedDto(this List<EventCategory>? values)
    {
        return values is null
            ? new List<DetailedEventCategoryDto>()
            : values.Select(x => x.ToDetailedDto()).ToList();
    }

    /// <summary>
    /// Преобразует сущность типа <see cref="EventCategory"/> в <see cref="EventCategoryDto"/>.
    /// </summary>
    /// <param name="value">Категория мероприятия.</param>
    /// <returns>Основная информация о категории мероприятия.</returns>
    public static EventCategoryDto ToDto(this EventCategory value)
    {
        return new EventCategoryDto
        {
            Id = value.Id,
            Name = value.Name,
            Order = value.Order,
            SectionId = value.SectionId
        };
    }
}
