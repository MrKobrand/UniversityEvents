namespace Application.Contracts.Users.Dto;

/// <summary>
/// Подробная информация о пользователе.
/// </summary>
public class DetailedUserDto : UserDto
{
    /// <summary>
    /// Ссылка на изображение аватара.
    /// </summary>
    public string? AvatarLink { get; set; }
}
