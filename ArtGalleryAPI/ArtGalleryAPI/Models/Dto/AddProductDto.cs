using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class AddProductDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Product Name can have a maximum of 100 characters!")]
        public string? Name { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Product Description can have a maximum of 500 characters!")]
        public string? Description { get; set; }

        [Required]
        [MaxLength(3000, ErrorMessage = "Product image url can have a maximum of 3000 characters!")]
        public string? ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
