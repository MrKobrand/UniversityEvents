using System.Text.Json.Serialization;

namespace MobileMaui.Services.UniversityEvents.Models.EventCategories;

public class GetEventCategoriesListUniversityEventsResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("sectionId")]
    public long SectionId { get; set; }

    [JsonPropertyName("sectionName")]
    public required string SectionName { get; set; }
}