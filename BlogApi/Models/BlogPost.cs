using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApi.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength =5)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000, MinimumLength =50)]
        public string Content { get; set; }
        [Url]
        public string ThumbnailUrl { get; set; }
        [Required]
        [StringLength(100)]
        public string Author { get; set; }
        public int ViewCount { get; set; } = 0;
        public bool IsPublished { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
