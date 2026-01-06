using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);

                entity.Property(c => c.Description).IsRequired().HasMaxLength(5000);

                entity.HasMany(c => c.Blogposts)
                       .WithOne(b => b.Category)
                       .HasForeignKey(b => b.CategoryId)
                       .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.ToTable("BlogPost");

                entity.HasKey(b => b.Id);

                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);

                entity.Property(b => b.Content).IsRequired().HasMaxLength(5000);

                entity.Property(b => b.Author).IsRequired().HasMaxLength(100);

                entity.Property(b => b.ViewCount).HasDefaultValue(0);

                entity.Property(b => b.IsPublished).HasDefaultValue(true);
            });
            SeedData(modelBuilder);

        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // 1. Tạo danh sách 10 Categories theo yêu cầu
            var categoryNames = new string[]
            {
        "Công Nghệ", "Lập Trình", "Web Dev", "Mobile", "DevOps",
        "AI", "Database", "Cloud", "Bảo Mật", "Khác"
            };

            var categories = new List<Category>();
            for (int i = 0; i < categoryNames.Length; i++)
            {
                categories.Add(new Category
                {
                    Id = i + 1,
                    Name = categoryNames[i],
                    Description = $"Chuyên mục chia sẻ kiến thức về {categoryNames[i]}",
                    CreatedAt = new DateTime (2026, 01, 06)
                });
            }
            modelBuilder.Entity<Category>().HasData(categories);

            // 2. Tạo danh sách 30 BlogPosts (3 bài cho mỗi Category)
            var posts = new List<BlogPost>();
            int postId = 1;

            for (int catId = 1; catId <= 10; catId++)
            {
                string catName = categoryNames[catId - 1];

                for (int p = 1; p <= 3; p++)
                {
                    posts.Add(new BlogPost
                    {
                        Id = postId,
                        Title = $"Hướng dẫn học {catName} cơ bản phần {p}", // Từ 5-200 ký tự
                        Content = $"Đây là nội dung chi tiết của bài viết về {catName}. Nội dung này được biên soạn kỹ lưỡng để đảm bảo dài hơn 50 ký tự nhằm đáp ứng yêu cầu kiểm tra dữ liệu đầu vào của hệ thống.", // Từ 50-5000 ký tự
                        ThumbnailUrl = $"https://example.com/images/{catName.ToLower()}-{p}.jpg", // Phải là URL hợp lệ
                        Author = "Admin Hệ Thống", // Tối đa 100 ký tự
                        ViewCount = postId * 8,
                        IsPublished = true, // Mặc định là true
                        CreatedAt = new DateTime(2026, 01, 06),
                        CategoryId = catId // Foreign Key liên kết với Category
                    });
                    postId++;
                }
            }
            modelBuilder.Entity<BlogPost>().HasData(posts);
        }
    }
}
