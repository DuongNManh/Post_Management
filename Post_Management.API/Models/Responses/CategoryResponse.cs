using System.Text.Json.Serialization;

namespace Post_Management.API.Models.Responses
{
    public class CategoryResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url_handle")]
        public string UrlHandle { get; set; }
    }
}
