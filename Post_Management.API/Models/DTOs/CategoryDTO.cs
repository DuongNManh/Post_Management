using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Post_Management.API.Models.DTOs
{
    public class CategoryDTO
    {
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [Required]
        [JsonPropertyName("url_handle")]
        public string UrlHandle { get; set; }

    }
}
