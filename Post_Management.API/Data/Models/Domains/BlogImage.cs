using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post_Management.API.Data.Models.Domains
{
    [Table("BlogImages")]
    public class BlogImage
    {
        [Key]
        [Column("id")]
        public Guid id { get; set; }

        [Required]
        [Column("file_name")]
        public string FileName { get; set; }

        [Required]
        [Column("file_extension")]
        public string FileExtension { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("url")]
        public string URl { get; set; }

        [Required]
        [Column("date_created", TypeName = "timestamp with time zone")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
