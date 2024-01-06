using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Contracts.Events.Dto;
using Domain.Common;
using Domain.Entities;

namespace Application.Contracts.Events;

/// <summary>
/// Сервис для работы с мероприятиями.
/// </summary>
public interface IEventService
{
    /// <summary>
    /// Получает мероприятие по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Мероприятие.</returns>
    Task<DetailedEventDto> GetAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает список мероприятий.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список мероприятий.</returns>
    Task<IEnumerable<DetailedEventDto>> GetListAsync(int? limit, string? search, CancellationToken cancellationToken);

    /// <summary>
    /// Получает страницу мероприятий.
    /// </summary>
    /// <param name="page">Номер страницы.</param>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Страница с мероприятиями.</returns>
    Task<PaginatedList<DetailedEventDto>> GetPageAsync(int? page, int? limit, string? search, CancellationToken cancellationToken);

    /// <summary>
    /// Получает типы мероприятия.
    /// </summary>
    /// <returns>Список типов мероприятия.</returns>
    IEnumerable<EnumValueModel> GetTypes();

    /// <summary>
    /// Создает мероприятие.
    /// </summary>
    /// <param name="event">Мероприятие, которое подлежит созданию.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Созданное мероприятие.</returns>
    Task<EventDto> CreateAsync(Event @event, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет мероприятие.
    /// </summary>
    /// <param name="event">Мероприятие, которое подлежит обновлению.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Обновленное мероприятие.</returns>
    Task<EventDto> UpdateAsync(Event @event, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет мероприятие.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Удаленное мероприятие.</returns>
    Task<EventDto> DeleteAsync(long id, CancellationToken cancellationToken);
}
