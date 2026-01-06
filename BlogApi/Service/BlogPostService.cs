using BlogApi.Data;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly AppDbContext _context;

        public BlogPostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _context.BlogPosts
                .Include(b => b.Category)
                .Where(b => b.IsPublished)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<BlogPost> GetByIdAsync(int id)
        {
            var post = await _context.BlogPosts
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (post == null) throw new KeyNotFoundException("Bài viết không tồn tại.");

            post.ViewCount++;
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<IEnumerable<BlogPost>> GetByCategoryAsync(int categoryId)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == categoryId);
            if (!categoryExists) throw new KeyNotFoundException("Danh mục không tồn tại.");

            return await _context.BlogPosts
                .Include(b => b.Category)
                .Where(b => b.CategoryId == categoryId && b.IsPublished)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            ValidateBlogPost(blogPost);

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == blogPost.CategoryId);
            if (!categoryExists) throw new ArgumentException("CategoryId không tồn tại.");

            blogPost.CreatedAt = DateTime.UtcNow;
            blogPost.ViewCount = 0;

            if (blogPost.IsPublished)
            {
                blogPost.PublishedAt = DateTime.UtcNow;
            }

            _context.BlogPosts.Add(blogPost);
            await _context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost> UpdateAsync(int id, BlogPost blogPost)
        {
            var existingPost = await _context.BlogPosts.FindAsync(id);
            if (existingPost == null) throw new KeyNotFoundException("Bài viết không tồn tại.");

            ValidateBlogPost(blogPost);

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == blogPost.CategoryId);
            if (!categoryExists) throw new ArgumentException("CategoryId không tồn tại.");

            existingPost.Title = blogPost.Title;
            existingPost.Content = blogPost.Content;
            existingPost.Author = blogPost.Author;
            existingPost.ThumbnailUrl = blogPost.ThumbnailUrl;
            existingPost.CategoryId = blogPost.CategoryId;
            existingPost.IsPublished = blogPost.IsPublished;
            existingPost.UpdatedAt = DateTime.UtcNow;

            if (blogPost.IsPublished && existingPost.PublishedAt == null)
            {
                existingPost.PublishedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return existingPost;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var post = await _context.BlogPosts.FindAsync(id);
            if (post == null) throw new KeyNotFoundException("Bài viết không tồn tại.");

            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        private void ValidateBlogPost(BlogPost blogPost)
        {
            if (string.IsNullOrWhiteSpace(blogPost.Title) || blogPost.Title.Length < 5 || blogPost.Title.Length > 200)
                throw new ArgumentException("Tiêu đề phải từ 5 đến 200 ký tự.");

            if (string.IsNullOrWhiteSpace(blogPost.Content) || blogPost.Content.Length < 50 || blogPost.Content.Length > 5000)
                throw new ArgumentException("Nội dung phải từ 50 đến 5000 ký tự.");

            if (string.IsNullOrWhiteSpace(blogPost.Author) || blogPost.Author.Length > 100)
                throw new ArgumentException("Tác giả không được trống và tối đa 100 ký tự.");

            // Kiểm tra ThumbnailUrl hợp lệ nếu có
            if (!string.IsNullOrEmpty(blogPost.ThumbnailUrl))
            {
                if (!Uri.IsWellFormedUriString(blogPost.ThumbnailUrl, UriKind.Absolute))
                    throw new ArgumentException("ThumbnailUrl không đúng định dạng URL.");
            }
        }
    }
}