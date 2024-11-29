using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Configuration;

/// <summary>
/// Настройки шаблонов сообщений.
/// </summary>
public class MessageTemplateOptions
{
    /// <summary>
    /// Шаблон для формирования сообщения о помощи в заполнении мероприятия.
    /// </summary>
    [Required]
    public required string EventHelp { get; set; }
}