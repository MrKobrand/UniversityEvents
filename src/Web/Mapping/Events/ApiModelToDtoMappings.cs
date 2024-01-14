using Application.Contracts.Events.Dto;
using Web.Contracts.Events;

namespace Web.Mapping.Events;

/// <summary>
/// Класс, содержащий расширения для преобразования моделей API запросов в DTO.
/// </summary>
public static class ApiModelToDtoMappings
{
    /// <summary>
    /// Преобразует запрос на создание мероприятия в DTO.
    /// </summary>
    /// <param name="command">Запрос на создание мероприятия.</param>
    /// <returns>DTO запроса на создание мероприятия.</returns>
    public static CreateEventRequestDto ToRequestDto(this CreateEventCommand command)
    {
        return new CreateEventRequestDto
        {
            Type = command.Type,
            Date = command.Date,
            Duration = command.Duration,
            Place = command.Place,
            Subject = command.Subject,
            Announcement = command.Announcement,
            Content = command.Content,
            PreviewImageId = command.PreviewImageId,
            CategoryId = command.CategoryId,
            AuthorId = command.AuthorId
        };
    }

    /// Преобразует запрос на обновление мероприятия в DTO.
    /// </summary>
    /// <param name="command">Запрос на обновление мероприятия.</param>
    /// <returns>DTO запроса на обновление мероприятия.</returns>
    public static UpdateEventRequestDto ToRequestDto(this UpdateEventCommand command, long id)
    {
        return new UpdateEventRequestDto
        {
            Id = id,
            Type = command.Type,
            Date = command.Date,
            Duration = command.Duration,
            Place = command.Place,
            Subject = command.Subject,
            Announcement = command.Announcement,
            Content = command.Content,
            PreviewImageId = command.PreviewImageId,
            CategoryId = command.CategoryId,
            AuthorId = command.AuthorId
        };
    }
}
