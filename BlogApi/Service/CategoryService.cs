using BlogApi.Data;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name) || category.Name.Length < 3 || category.Name.Length > 100)
            {
                throw new ArgumentException("Tên danh mục không được trống và phải từ 3 đến 100 ký tự.");
            }

            var isNameExists = await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == category.Name.ToLower());
            if (isNameExists)
            {
                throw new ArgumentException($"Tên danh mục '{category.Name}' đã tồn tại.");
            }

            category.CreatedAt = DateTime.UtcNow; 
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Blogposts)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy danh mục với ID {id} để xóa.");
            }

            // Business Logic: Nếu có bài viết thì không cho xóa
            if (category.Blogposts != null && category.Blogposts.Any())
            {
                throw new InvalidOperationException(
                    $"Không thể xóa danh mục '{category.Name}' vì nó chứa {category.Blogposts.Count} bài viết");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(c => c.Blogposts).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _context.Categories.Include(c => c.Blogposts).FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);

            if (existingCategory == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy danh mục với ID {id}");
            }

            if (string.IsNullOrWhiteSpace(category.Name) || category.Name.Length < 3 || category.Name.Length > 100)
            {
                throw new ArgumentException("Tên danh mục hợp lệ phải từ 3 đến 100 ký tự.");
            }

            var isNameDuplicate = await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == category.Name.ToLower() && c.Id != id);
            if (isNameDuplicate)
            {
                throw new ArgumentException($"Tên danh mục '{category.Name}' đã được sử dụng bởi danh mục khác.");
            }

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
            existingCategory.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingCategory;
        }
    }
}
