using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Post_Management.API.Models.DTOs
{
    public class BlogImageDTO
    {
        [JsonPropertyName("file")]
        [Required(ErrorMessage = "File is required")]
        public IFormFile File { get; set; }
        [JsonPropertyName("file_name")]
        [Required(ErrorMessage = "File name is required")]
        public string FileName { get; set; }
        [JsonPropertyName("title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
    }
}
