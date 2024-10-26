using System.Text.Json.Serialization;

namespace MobileMaui.Services.UniversityEvents.Models;

public class GetEventSectionsListUniversityEventsRequest
{
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    [JsonPropertyName("search")]
    public string? Search { get; set; }
}