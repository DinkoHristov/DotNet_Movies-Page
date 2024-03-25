using System.Text.Json.Serialization;

namespace MoviesPage.DataInitialization.DataInitialization.ImportModels
{
    public class MovieDto
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonPropertyName("cast")]
        public List<string>? CastNames { get; set; }

        [JsonPropertyName("genres")]
        public List<string> Genres { get; set; }
    }
}
