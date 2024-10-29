using System.Text.Json.Serialization;

namespace MobileMaui.Services.UniversityEvents.Models.Events;

public class DetailedEventUniversityEventsModel : EventUniversityEventsModel
{
    [JsonPropertyName("previewImageLink")]
    public string? PreviewImageLink { get; set; }

    [JsonPropertyName("authorFirstName")]
    public required string AuthorFirstName { get; set; }

    [JsonPropertyName("authorLastName")]
    public required string AuthorLastName { get; set; }

    [JsonPropertyName("authorMiddleName")]
    public string? AuthorMiddleName { get; set; }

    [JsonPropertyName("authorAvatarId")]
    public long? AuthorAvatarId { get; set; }

    [JsonPropertyName("authorAvatarLink")]
    public string? AuthorAvatarLink { get; set; }

    [JsonPropertyName("speakers")]
    public required IReadOnlyList<EventUserUniversityEventsModel> Speakers { get; set; }

    [JsonPropertyName("participants")]
    public required IReadOnlyList<EventUserUniversityEventsModel> Participants { get; set; }
}