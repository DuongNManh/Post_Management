using System.Text.Json.Serialization;

namespace Post_Management.API.Models.Domains
{
    public class Category
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("url_handle")]
        public string UrlHandle { get; set; }
    }
}
