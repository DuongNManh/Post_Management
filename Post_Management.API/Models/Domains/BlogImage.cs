namespace Post_Management.API.Models.Domains
{
    public class BlogImage
    {
        public Guid id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Title { get; set; }
        public string URl { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
