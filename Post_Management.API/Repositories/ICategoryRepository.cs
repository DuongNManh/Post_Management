using Post_Management.API.Models.Domains;

namespace Post_Management.API.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(Guid id);
        Task<Category> GetCategoryByUrlHandle(string urlHandle);
        Task<Category> CreateCategory(Category category);
        Task<Category?> UpdateCategory(Guid id, Category category);
        Task<Category?> DeleteCategory(Guid id);
    }
}
