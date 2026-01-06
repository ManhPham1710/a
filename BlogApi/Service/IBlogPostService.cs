using BlogApi.Models;

namespace BlogApi.Services
{
    public interface IBlogPostService
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost> GetByIdAsync(int id);
        Task<IEnumerable<BlogPost>> GetByCategoryAsync(int categoryId);
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<BlogPost> UpdateAsync(int id, BlogPost blogPost);
        Task<bool> DeleteAsync(int id);
    }
}