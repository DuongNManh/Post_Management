using Post_Management.API.Models.Domains;

namespace Post_Management.API.Repositories
{
    public interface IImageRepository
    {
        Task<BlogImage> AddImageAsync(IFormFile file, BlogImage image);
        Task<IEnumerable<BlogImage>> GetAllImagesAsync();
    }
}
