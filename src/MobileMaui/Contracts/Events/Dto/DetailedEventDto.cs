namespace MobileMaui.Contracts.Events.Dto;

/// <summary>
/// Подробное мероприятие.
/// </summary>
public class DetailedEventDto : EventDto
{
    /// <summary>
    /// Ссылка на изображение превью в файловом хранилище.
    /// </summary>
    public string? PreviewImageLink { get; set; }

    /// <summary>
    /// Данные изображения превью.
    /// </summary>
    public byte[]? PreviewImageData { get; set; }

    /// <summary>
    /// Имя автора.
    /// </summary>
    public required string AuthorFirstName { get; set; }

    /// <summary>
    /// Фамилия автора.
    /// </summary>
    public required string AuthorLastName { get; set; }

    /// <summary>
    /// Отчество автора.
    /// </summary>
    public string? AuthorMiddleName { get; set; }

    /// <summary>
    /// Уникальный идентификатор аватара автора.
    /// </summary>
    public long? AuthorAvatarId { get; set; }

    /// <summary>
    /// Ссылка на аватар автора.
    /// </summary>
    public string? AuthorAvatarLink { get; set; }

    /// <summary>
    /// Данные аватара автора.
    /// </summary>
    public byte[]? AuthorAvatarData { get; set; }

    /// <summary>
    /// Спикеры.
    /// </summary>
    public required IReadOnlyList<EventUserDto> Speakers { get; set; }

    /// <summary>
    /// Участники.
    /// </summary>
    public required IReadOnlyList<EventUserDto> Participants { get; set; }
}