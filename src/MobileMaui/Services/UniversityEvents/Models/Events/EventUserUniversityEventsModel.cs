using System.Text.Json.Serialization;

namespace MobileMaui.Services.UniversityEvents.Models.Events;

public class EventUserUniversityEventsModel
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("firstName")]
    public required string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public required string LastName { get; set; }

    [JsonPropertyName("middleName")]
    public string? MiddleName { get; set; }

    [JsonPropertyName("avatarId")]
    public long? AvatarId { get; set; }

    [JsonPropertyName("avatarLink")]
    public string? AvatarLink { get; set; }
}