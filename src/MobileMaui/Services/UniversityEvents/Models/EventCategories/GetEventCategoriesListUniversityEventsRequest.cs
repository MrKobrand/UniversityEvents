using System.Text.Json.Serialization;

namespace MobileMaui.Services.UniversityEvents.Models.EventCategories;

public class GetEventCategoriesListUniversityEventsRequest
{
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    [JsonPropertyName("search")]
    public string? Search { get; set; }

    [JsonPropertyName("sectionId")]
    public long? SectionId { get; set; }
}