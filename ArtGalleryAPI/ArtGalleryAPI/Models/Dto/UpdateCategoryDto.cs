using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class UpdateCategoryDto
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Category name can have a maximum of 50 characters!")]
        public required string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Category description by can have a maximum of 500 characters!")]
        public string? Description { get; set; }
    }
}
