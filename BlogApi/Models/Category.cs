using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace BlogApi.Models
{
    public class Category
    {
        [Key]   
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength =3)]
        public string Name { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        [JsonIgnore]
        public ICollection<BlogPost> Blogposts { get; set; } = new List<BlogPost>();

    }
}
