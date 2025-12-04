using Library_BackEnd.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Library_BackEnd.Services
{
    public class CategoryService
    {

        private ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCategory(Category category)
        {
            _context.Categories.Add(category);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Category?> GetCategoryByName(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name.Equals(name));
        }

        public async Task<Category?> GetCategoryById(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            //return await _context.SaveChangesAsync() > 0;

            var existingCategory = await GetCategoryById(category.Id);

            if (existingCategory == null)
            {
                return false;
            }

            existingCategory.Name = category.Name;
            existingCategory.CreatedAt = category.CreatedAt;
            existingCategory.Books = category.Books;

            return _context.SaveChanges() > 0;

        }

        public async Task<bool> DeleteCategory(Guid id)
        {

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
