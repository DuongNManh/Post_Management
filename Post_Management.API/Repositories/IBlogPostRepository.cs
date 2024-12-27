using Post_Management.API.Models.Domains;

namespace Post_Management.API.Repositories
{
    public interface IBlogPostRepository
    {
        public Task<IEnumerable<BlogPost>> GetAllBlogPosts();
        public Task<BlogPost> CreateBlogPost(BlogPost blogPost);
        public Task<BlogPost?> GetBlogPostById(Guid id);
        public Task<BlogPost?> DeleteBlogPost(Guid id);
        public Task<BlogPost?> UpdateBlogPost(Guid id, BlogPost blogPost);
    }
}
