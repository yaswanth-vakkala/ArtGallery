using ArtGalleryAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class AddWishlistDto
    {
        [Required]
        public required Guid ProductId { get; set; }

        [Required]
        public required string AppUserId { get; set; }
    }
}
