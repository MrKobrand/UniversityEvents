using System.Text.Json.Serialization;

namespace MobileMaui.Services.UniversityEvents.Models.Events;

public class GetEventsListUniversityEventsRequest
{
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    [JsonPropertyName("search")]
    public string? Search { get; set; }

    [JsonPropertyName("categoryId")]
    public long? CategoryId { get; set; }
}