using Microsoft.EntityFrameworkCore;
using Post_Management.API.Data;
using Post_Management.API.Models.Domains;

namespace Post_Management.API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        // Constructor
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            try
            {
                await _dbContext.Categories.AddAsync(category);
                await _dbContext.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category?> DeleteCategory(Guid id)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCategory == null)
            {
                return null;
            }
            try
            {
                _dbContext.Categories.Remove(existingCategory);
                await _dbContext.SaveChangesAsync();
                return existingCategory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            try
            {
                return await _dbContext.Categories.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category?> GetCategoryById(Guid id)
        {
            try
            {
                return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Category> GetCategoryByUrlHandle(string urlHandle)
        {
            throw new NotImplementedException();
        }

        public async Task<Category?> UpdateCategory(Guid id, Category category)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCategory == null)
            {
                return null;
            }
            try
            {
                // Ensure the ID is not updated
                category.Id = existingCategory.Id;
                _dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await _dbContext.SaveChangesAsync();
                return existingCategory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ValidateCategories(IEnumerable<Guid> categoryIds)
        {
            var validCategories = await _dbContext.Categories
                .Where(c => categoryIds.Contains(c.Id))
                .CountAsync();
            return validCategories == categoryIds.Count();
        }

        public async Task<IEnumerable<Category>> GetCategoriesByIds(IEnumerable<Guid> categoryIds)
        {
            try
            {
                return await _dbContext.Categories
                    .Where(c => categoryIds.Contains(c.Id))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
