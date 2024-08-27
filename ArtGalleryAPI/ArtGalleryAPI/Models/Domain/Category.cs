using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Domain
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Category name can have a maximum of 50 characters!")]
        public required string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Category description by can have a maximum of 500 characters!")]
        public string? Description { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedAt { get; set; }

        [MaxLength(100, ErrorMessage = "Category modified by can have a maximum of 100 characters!")]
        public string? ModifiedBy { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
