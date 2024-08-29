using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryAPI.Models.Domain
{
    public class WishList
    {
        [Key]
        public Guid WishListId { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public required Guid ProductId { get; set; }

        [Required]
        public required string AppUserId { get; set; }
    }
}
