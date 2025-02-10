using Microsoft.EntityFrameworkCore;
using Post_Management.API.Data;
using Post_Management.API.Data.Models.Domains;

namespace Post_Management.API.Repositories
{
    public class BlogPostRepository(ApplicationDbContext dbContext) : IBlogPostRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<BlogPost> CreateBlogPost(BlogPost blogPost)
        {
            try
            {
                await _dbContext.BlogPosts.AddAsync(blogPost);
                await _dbContext.SaveChangesAsync();
                return blogPost;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BlogPost?> DeleteBlogPost(Guid id)
        {
            try
            {
                var existingBlogPost = await _dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
                if (existingBlogPost == null)
                {
                    return null;
                }
                _dbContext.BlogPosts.Remove(existingBlogPost);
                await _dbContext.SaveChangesAsync();
                return existingBlogPost;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPosts()
        {
            try
            {
                return await _dbContext.BlogPosts
                    .Include(x => x.Categories)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BlogPost?> GetBlogPostById(Guid id)
        {
            try
            {
                return await _dbContext.BlogPosts
                    .Include(x => x.Categories)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BlogPost?> GetBlogPostByUrl(string urlHandle)
        {
            return await _dbContext.BlogPosts
                .Include(x => x.Categories)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateBlogPost(Guid id, BlogPost blogPost)
        {
            try
            {
                var existingBlogPost = await _dbContext.BlogPosts
                    .Include(x => x.Categories)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (existingBlogPost == null)
                {
                    return null;
                }

                // Update basic properties
                existingBlogPost.Title = blogPost.Title;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.PublishDate = blogPost.PublishDate;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.Author = blogPost.Author;

                // Update categories
                // Clear existing categories
                existingBlogPost.Categories.Clear();

                // Add new categories
                if (blogPost.Categories != null && blogPost.Categories.Any())
                {
                    foreach (var category in blogPost.Categories)
                    {
                        existingBlogPost.Categories.Add(category);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return existingBlogPost;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
