using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Post_Management.API.Data.Models.DTOs
{
    public class BlogPostDTO
    {
        //[Required]
        [MinLength(length: 3, ErrorMessage = "Min length title is 3")]
        [MaxLength(length: 100, ErrorMessage = "Max length title is 100")]
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [Required]
        [JsonPropertyName("short_description")]
        public string ShortDescription { get; set; }
        [Required]
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("featured_image_url")]
        public string? FeaturedImageUrl { get; set; }
        [JsonPropertyName("url_handle")]
        [Required]
        public string UrlHandle { get; set; }
        [JsonPropertyName("publish_date")]
        [Required]
        public string PublishDate { get; set; }
        [JsonPropertyName("author")]
        [Required]
        public string Author { get; set; }
        [JsonPropertyName("is_visible")]
        public bool IsVisible { get; set; }
        [JsonPropertyName("categories")]
        public ICollection<Guid> Categories { get; set; }
    }
}
