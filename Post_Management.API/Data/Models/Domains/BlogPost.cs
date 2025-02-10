using System.Text.Json.Serialization;

namespace Post_Management.API.Data.Models.Domains
{
    public class BlogPost
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("short_description")]
        public string ShortDescription { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("featured_image_url")]
        public string FeaturedImageUrl { get; set; }
        [JsonPropertyName("url_handle")]
        public string UrlHandle { get; set; }
        [JsonPropertyName("publish_date")]
        public DateTime PublishDate { get; set; }
        [JsonPropertyName("author")]
        public string Author { get; set; }
        [JsonIgnore]
        public bool IsVisible { get; set; }
        [JsonPropertyName("categories")]
        public ICollection<Category> Categories { get; set; }
    }
}
