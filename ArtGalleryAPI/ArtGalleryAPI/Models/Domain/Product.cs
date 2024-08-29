using ArtGalleryAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ArtGalleryAPI.Models.Domain
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Product Name can have a maximum of 100 characters!")]
        public required string Name { get; set; }

        [Required]
        [MaxLength(4000, ErrorMessage = "Product Description can have a maximum of 4000 characters!")]
        public required string Description { get; set; }

        [Required]
        [MaxLength(4000, ErrorMessage = "Product image url can have a maximum of 4000 characters!")]
        public required string ImageUrl { get; set; }

        [Required]
        [Precision(13, 3)]
        public required decimal Price { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Product status can have a maximum of 30 characters!")]
        public required string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedAt { get; set; }

        [MaxLength(100, ErrorMessage = "Product modified by can have a maximum of 100 characters!")]
        public string? ModifiedBy { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public required Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}