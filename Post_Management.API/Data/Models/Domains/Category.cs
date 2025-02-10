using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Post_Management.API.Data.Models.Domains
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [Column("id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [Column("url_handle")]
        [JsonPropertyName("url_handle")]
        public string UrlHandle { get; set; }

        [JsonIgnore]
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}
