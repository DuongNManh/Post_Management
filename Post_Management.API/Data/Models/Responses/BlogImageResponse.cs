using System.Text.Json.Serialization;

namespace Post_Management.API.Data.Models.Responses
{
    public class BlogImageResponse
    {
        [JsonPropertyName("id")]
        public Guid id { get; set; }
        [JsonPropertyName("file_name")]
        public string FileName { get; set; }
        [JsonPropertyName("file_extension")]
        public string FileExtension { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("url")]
        public string URl { get; set; }
        [JsonPropertyName("date_created")]
        public string DateCreated { get; set; }
    }
}
