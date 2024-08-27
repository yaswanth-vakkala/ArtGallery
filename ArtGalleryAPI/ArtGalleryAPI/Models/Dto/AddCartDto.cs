using ArtGalleryAPI.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class AddCartDto
    {
        [Required]
        public required int Quantity { get; set; }

        [Required]
        public required Guid ProductId { get; set; }

        [Required]
        public required string AppUserId { get; set; }
    }
}
