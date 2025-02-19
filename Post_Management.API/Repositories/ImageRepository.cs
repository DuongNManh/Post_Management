using Microsoft.EntityFrameworkCore;
using Post_Management.API.Data;
using Post_Management.API.Data.Models.Domains;
using Microsoft.Extensions.Configuration;

namespace Post_Management.API.Repositories
{
    public class ImageRepository(
        IWebHostEnvironment webHostEnvironment,
        IHttpContextAccessor httpContextAccessor,
        ApplicationDbContext dbContext,
        IConfiguration configuration) : IImageRepository
    {

        public async Task<BlogImage> AddImageAsync(IFormFile file, BlogImage image)
        {
            try
            {
                //generate a unique id for the image
                image.id = Guid.NewGuid();

                // Upload the image to API/Images (local storage)
                var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.id}{image.FileExtension}");
                using var stream = new FileStream(localPath, FileMode.Create);
                await file.CopyToAsync(stream);

                // Get the host and scheme from the current request
                var httpReq = httpContextAccessor.HttpContext?.Request
                    ?? throw new InvalidOperationException("HttpContext or HttpRequest is null.");

                // For Docker environment, use localhost for external access
                var externalHost = configuration["EXTERNAL_API_HOST"] ?? "localhost:5001";
                var urlPath = $"http://{externalHost}/Images/{image.id}{image.FileExtension}";
                image.URl = urlPath;

                // Ensure DateCreated is in UTC
                image.DateCreated = DateTime.UtcNow;

                await dbContext.BlogImages.AddAsync(image);
                await dbContext.SaveChangesAsync();
                return image;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<BlogImage>> GetAllImagesAsync()
        {
            return await dbContext.BlogImages.ToListAsync();
        }
    }
}
