using Microsoft.EntityFrameworkCore;
using Post_Management.API.Data;
using Post_Management.API.Models.Domains;

namespace Post_Management.API.Repositories
{
    public class ImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext) : IImageRepository
    {

        public async Task<BlogImage> AddImageAsync(IFormFile file, BlogImage image)
        {
            try
            {
                // Upload the image to API/Images (local storage)
                var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
                using var stream = new FileStream(localPath, FileMode.Create);
                await file.CopyToAsync(stream);
                // Save the image details to the database
                var httpReq = (httpContextAccessor.HttpContext?.Request) ?? throw new InvalidOperationException("HttpContext or HttpRequest is null.");
                var urlPath = $"{httpReq.Scheme}://{httpReq.Host}{httpReq.PathBase}/Images/{image.FileName}{image.FileExtension}";
                image.URl = urlPath;
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
