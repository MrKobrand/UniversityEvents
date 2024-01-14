using System.Collections.Generic;
using System.Linq;
using Application.Contracts.EventSections.Dto;
using Domain.Entities;

namespace Infrastructure.Services.EventSections.Extensions;

/// <summary>
/// Класс, содержащий расширения для преобразования типа
/// раздела мероприятия <see cref="EventSection"/> в производные DTO.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Преобразует сущность типа <see cref="EventSection"/> в <see cref="EventSectionDto"/>.
    /// </summary>
    /// <param name="value">Раздел мероприятия.</param>
    /// <returns>Информация о разделе мероприятия.</returns>
    public static EventSectionDto ToDto(this EventSection value)
    {
        return new EventSectionDto
        {
            Id = value.Id,
            Name = value.Name,
            Description = value.Description,
            Order = value.Order
        };
    }

    /// <summary>
    /// Преобразует список сущностей типа <see cref="EventSection"/> в <see cref="EventSectionDto"/>.
    /// </summary>
    /// <param name="values">Разделы мероприятий.</param>
    /// <returns>Информация о разделах мероприятий.</returns>
    public static List<EventSectionDto> ToDto(this List<EventSection>? values)
    {
        return values is null
            ? new List<EventSectionDto>()
            : values.Select(x => x.ToDto()).ToList();
    }
}
