using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Contracts.Events.Dto;
using Domain.Common;

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
    Task<DetailedEventDto?> GetAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает подсказку от ИИ для заполнения содержания мероприятия при его создании.
    /// </summary>
    /// <param name="request">Тема мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Пример заполненного мероприятия.</returns>
    Task<EventExampleDto> GetHelpAsync(string request, CancellationToken cancellationToken);

    /// <summary>
    /// Получает список мероприятий.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="categoryId">Уникальный идентификатор категории мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список мероприятий.</returns>
    Task<List<DetailedEventDto>> GetListAsync(
        int? limit,
        string? search,
        long? categoryId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получает страницу мероприятий.
    /// </summary>
    /// <param name="page">Номер страницы.</param>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="categoryId">Уникальный идентификатор категории мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Страница с мероприятиями.</returns>
    Task<PaginatedList<DetailedEventDto>> GetPageAsync(
        int? page,
        int? limit,
        string? search,
        long? categoryId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получает типы мероприятия.
    /// </summary>
    /// <returns>Список типов мероприятия.</returns>
    List<EnumValueModel> GetTypes();

    /// <summary>
    /// Создает мероприятие.
    /// </summary>
    /// <param name="request">Запрос на создание мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Созданное мероприятие.</returns>
    Task<EventDto> CreateAsync(CreateEventRequestDto request, CancellationToken cancellationToken);

    /// <summary>
    /// Обновляет мероприятие.
    /// </summary>
    /// <param name="request">Запрос на обновление мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Обновленное мероприятие.</returns>
    Task<EventDto> UpdateAsync(UpdateEventRequestDto request, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет мероприятие.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Удаленное мероприятие.</returns>
    Task<EventDto?> DeleteAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Прикрепляет участника к мероприятию.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="participantId">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о прикреплении участника к мероприятию.</returns>
    Task<EventParticipantDto> AssignParticipantAsync(long id, long participantId, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет участника мероприятия.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="participantId">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация об удаленном участнике мероприятия.</returns>
    Task<EventParticipantDto?> RemoveParticipantAsync(long id, long participantId, CancellationToken cancellationToken);

    /// <summary>
    /// Прикрепляет спикера к мероприятию.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="speakerId">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о прикреплении спикера к мероприятию.</returns>
    Task<EventSpeakerDto> AssignSpeakerAsync(long id, long speakerId, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет спикера мероприятия.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="speakerId">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация об удаленном спикере мероприятия.</returns>
    Task<EventSpeakerDto?> RemoveSpeakerAsync(long id, long speakerId, CancellationToken cancellationToken);
}
