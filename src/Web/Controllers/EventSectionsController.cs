using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.EventSections;
using Application.Contracts.EventSections.Dto;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.EventSections;
using Web.Infrastructure;

namespace Web.Controllers;

/// <summary>
/// Контроллер разделов мероприятий.
/// </summary>
public class EventSectionsController : ApiControllerBase
{
    private readonly IEventSectionService _eventSectionService;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="eventSectionService">Сервис для работы с разделами мероприятий.</param>
    public EventSectionsController(IEventSectionService eventSectionService)
    {
        _eventSectionService = eventSectionService;
    }

    /// <summary>
    /// Получает раздел мероприятия по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор раздела мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о разделе мероприятия.</returns>
    [HttpGet("{id:long}")]
    public Task<EventSectionDto?> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        return _eventSectionService.GetAsync(id, cancellationToken);
    }

    /// <summary>
    /// Получает список разделов мероприятий.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список сущностей с информацией о разделах мероприятий.</returns>
    [HttpGet]
    public Task<List<EventSectionDto>> GetList(
        [FromQuery] int? limit,
        [FromQuery] string? search,
        CancellationToken cancellationToken)
    {
        return _eventSectionService.GetListAsync(limit, search, cancellationToken);
    }

    /// <summary>
    /// Создает раздел мероприятия.
    /// </summary>
    /// <param name="command">Параметры на создание раздела мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о разделе мероприятия.</returns>
    [HttpPost]
    [Authorization(RoleType.Administrator)]
    public Task<EventSectionDto> Create(
        [FromBody] CreateEventSectionCommand command,
        CancellationToken cancellationToken)
    {
        return _eventSectionService.CreateAsync(command.Name, command.Description, cancellationToken);
    }

    /// <summary>
    /// Удаляет раздел мероприятия.
    /// </summary>
    /// <param name="id">Уникальный идентификатор раздела мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о разделе мероприятия.</returns>
    [HttpDelete("{id:long}")]
    [Authorization(RoleType.Administrator)]
    public Task<EventSectionDto?> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        return _eventSectionService.DeleteAsync(id, cancellationToken);
    }
}
