using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Post_Management.API.Data.Models.Domains
{
    [Table("BlogPosts")]
    public class BlogPost
    {
        [Key]
        [Column("id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("title")]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [Required]
        [Column("short_description")]
        [JsonPropertyName("short_description")]
        public string ShortDescription { get; set; }

        [Required]
        [Column("content")]
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [Column("featured_image_url")]
        [JsonPropertyName("featured_image_url")]
        public string FeaturedImageUrl { get; set; }

        [Required]
        [Column("url_handle")]
        [JsonPropertyName("url_handle")]
        public string UrlHandle { get; set; }

        [Required]
        [Column("publish_date", TypeName = "timestamp with time zone")]
        [JsonPropertyName("publish_date")]
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("author")]
        [JsonPropertyName("author")]
        public string Author { get; set; }

        [Required]
        [Column("is_visible")]
        [JsonIgnore]
        public bool IsVisible { get; set; }

        [JsonPropertyName("categories")]
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
