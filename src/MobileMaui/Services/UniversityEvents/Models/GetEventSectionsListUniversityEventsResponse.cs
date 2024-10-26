using System.Text.Json.Serialization;

namespace MobileMaui.Services.UniversityEvents.Models;

public class GetEventSectionsListUniversityEventsResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }
}