using System.Text.Json.Serialization;

namespace MobileMaui.Services.UniversityEvents.Models.Events;

public class EventUniversityEventsModel
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("date")]
    public DateTimeOffset Date { get; set; }

    [JsonPropertyName("duration")]
    public TimeSpan Duration { get; set; }

    [JsonPropertyName("place")]
    public required string Place { get; set; }

    [JsonPropertyName("subject")]
    public required string Subject { get; set; }

    [JsonPropertyName("announcement")]
    public string? Announcement { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("previewImageId")]
    public long? PreviewImageId { get; set; }

    [JsonPropertyName("categoryId")]
    public long CategoryId { get; set; }

    [JsonPropertyName("authorId")]
    public long AuthorId { get; set; }
}