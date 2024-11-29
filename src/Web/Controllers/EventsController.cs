using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Contracts.Events;
using Application.Contracts.Events.Dto;
using Domain.Common;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.Events;
using Web.Infrastructure;
using Web.Mapping.Events;

namespace Web.Controllers;

/// <summary>
/// Контроллер мероприятий.
/// </summary>
public class EventsController : ApiControllerBase
{
    private readonly IEventService _eventService;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="eventService">Сервис для работы с мероприятиями.</param>
    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }

    /// <summary>
    /// Получает мероприятие по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о мероприятии.</returns>
    [HttpGet("{id:long}")]
    public Task<DetailedEventDto?> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        return _eventService.GetAsync(id, cancellationToken);
    }

    /// <summary>
    /// Получает подсказку от ИИ для заполнения содержания мероприятия при его создании.
    /// </summary>
    /// <param name="request">Тема мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Пример заполненного мероприятия.</returns>
    [HttpGet("help")]
    [Authorization(RoleType.Administrator)]
    public Task<EventExampleDto> GetHelp([FromQuery] string request, CancellationToken cancellationToken)
    {
        return _eventService.GetHelpAsync(request, cancellationToken);
    }

    /// <summary>
    /// Получает список мероприятий.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="categoryId">Уникальный идентификатор категории мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список сущностей с информацией о мероприятиях.</returns>
    [HttpGet]
    public Task<List<DetailedEventDto>> GetList(
        [FromQuery] int? limit,
        [FromQuery] string? search,
        [FromQuery] long? categoryId,
        CancellationToken cancellationToken)
    {
        return _eventService.GetListAsync(limit, search, categoryId, cancellationToken);
    }

    /// <summary>
    /// Получает список мероприятий с пагинацией.
    /// </summary>
    /// <param name="page">Номер страницы.</param>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="categoryId">Уникальный идентификатор категории мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список сущностей с информацией о мероприятиях с пагинацией.</returns>
    [HttpGet("page")]
    public Task<PaginatedList<DetailedEventDto>> GetPage(
        [FromQuery] int? page,
        [FromQuery] int? limit,
        [FromQuery] string? search,
        [FromQuery] long? categoryId,
        CancellationToken cancellationToken)
    {
        return _eventService.GetPageAsync(page, limit, search, categoryId, cancellationToken);
    }

    /// <summary>
    /// Получает типы мероприятия.
    /// </summary>
    /// <returns>Список типов мероприятия.</returns>
    [HttpGet("types")]
    public List<EnumValueModel> GetTypes()
    {
        return _eventService.GetTypes();
    }

    /// <summary>
    /// Создает мероприятие.
    /// </summary>
    /// <param name="command">Параметры на создание мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о мероприятии.</returns>
    [HttpPost]
    [Authorization(RoleType.Administrator)]
    public Task<EventDto> Create(
        [FromBody] CreateEventCommand command,
        CancellationToken cancellationToken)
    {
        return _eventService.CreateAsync(command.ToRequestDto(), cancellationToken);
    }

    /// <summary>
    /// Обновляет мероприятие.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="command">Параметры на обновление мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    [HttpPut("{id:long}")]
    [Authorization(RoleType.Administrator)]
    public Task<EventDto> Update(
        [FromRoute] long id,
        [FromBody] UpdateEventCommand command,
        CancellationToken cancellationToken)
    {
        return _eventService.UpdateAsync(command.ToRequestDto(id), cancellationToken);
    }

    /// <summary>
    /// Удаляет мероприятие.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о мероприятии.</returns>
    [HttpDelete("{id:long}")]
    [Authorization(RoleType.Administrator)]
    public Task<EventDto?> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        return _eventService.DeleteAsync(id, cancellationToken);
    }

    /// <summary>
    /// Прикрепляет участника к мероприятию.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="participantId">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о прикреплении участника к мероприятию.</returns>
    [HttpPatch("{id:long}/participant/{participantId:long}")]
    [Authorization(RoleType.Administrator)]
    public Task<EventParticipantDto> AssignParticipant(
        [FromRoute] long id,
        [FromRoute] long participantId,
        CancellationToken cancellationToken)
    {
        return _eventService.AssignParticipantAsync(id, participantId, cancellationToken);
    }

    /// <summary>
    /// Удаляет участника мероприятия.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="participantId">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация об удаленном участнике мероприятия.</returns>
    [HttpDelete("{id:long}/participant/{participantId:long}")]
    [Authorization(RoleType.Administrator)]
    public Task<EventParticipantDto?> RemoveParticipant(
        [FromRoute] long id,
        [FromRoute] long participantId,
        CancellationToken cancellationToken)
    {
        return _eventService.RemoveParticipantAsync(id, participantId, cancellationToken);
    }

    /// <summary>
    /// Прикрепляет спикера к мероприятию.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="speakerId">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о прикреплении спикера к мероприятию.</returns>
    [HttpPatch("{id:long}/speaker/{speakerId:long}")]
    [Authorization(RoleType.Administrator)]
    public Task<EventSpeakerDto> AssignSpeaker(
        [FromRoute] long id,
        [FromRoute] long speakerId,
        CancellationToken cancellationToken)
    {
        return _eventService.AssignSpeakerAsync(id, speakerId, cancellationToken);
    }

    /// <summary>
    /// Удаляет спикера мероприятия.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="speakerId">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация об удаленном спикере мероприятия.</returns>
    [HttpDelete("{id:long}/speaker/{speakerId:long}")]
    [Authorization(RoleType.Administrator)]
    public Task<EventSpeakerDto?> RemoveSpeaker(
        [FromRoute] long id,
        [FromRoute] long speakerId,
        CancellationToken cancellationToken)
    {
        return _eventService.RemoveSpeakerAsync(id, speakerId, cancellationToken);
    }
}
