using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.EventCategories;
using Application.Contracts.EventCategories.Dto;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.EventCategories;
using Web.Infrastructure;

namespace Web.Controllers;

/// <summary>
/// Контроллер разделов мероприятий.
/// </summary>
public class EventCategoriesController : ApiControllerBase
{
    private readonly IEventCategoryService _eventCategoryService;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="eventCategoryService">Сервис для работы с категориями мероприятий.</param>
    public EventCategoriesController(IEventCategoryService eventCategoryService)
    {
        _eventCategoryService = eventCategoryService;
    }

    /// <summary>
    /// Получает категорию мероприятия по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор категории мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Подробная информация о категории мероприятия.</returns>
    [HttpGet("{id:long}")]
    public Task<DetailedEventCategoryDto?> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        return _eventCategoryService.GetAsync(id, cancellationToken);
    }

    /// <summary>
    /// Получает список категорий мероприятий.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="sectionId">Уникальный идентификатор раздела мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список сущностей с подробной информацией о категориях мероприятий.</returns>
    [HttpGet]
    public Task<List<DetailedEventCategoryDto>> GetList(
        [FromQuery] int? limit,
        [FromQuery] string? search,
        [FromQuery] long? sectionId,
        CancellationToken cancellationToken)
    {
        return _eventCategoryService.GetListAsync(limit, search, sectionId, cancellationToken);
    }

    /// <summary>
    /// Создает категорию мероприятия.
    /// </summary>
    /// <param name="command">Параметры на создание категории мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о категории мероприятия.</returns>
    [HttpPost]
    [Authorization(RoleType.Administrator)]
    public Task<EventCategoryDto> Create(
        [FromBody] CreateEventCategoryCommand command,
        CancellationToken cancellationToken)
    {
        return _eventCategoryService.CreateAsync(command.Name, command.SectionId, cancellationToken);
    }

    /// <summary>
    /// Удаляет категорию мероприятия.
    /// </summary>
    /// <param name="id">Уникальный идентификатор категории мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о категории мероприятия.</returns>
    [HttpDelete("{id:long}")]
    [Authorization(RoleType.Administrator)]
    public Task<EventCategoryDto?> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        return _eventCategoryService.DeleteAsync(id, cancellationToken);
    }
}
