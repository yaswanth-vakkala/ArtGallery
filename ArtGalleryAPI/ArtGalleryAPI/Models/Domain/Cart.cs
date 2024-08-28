using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryAPI.Models.Domain
{
    public class Cart
    {
        [Key]
        public Guid CartId { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; }

        [Required]
        public required Guid ProductId { get; set; }

        [Required]
        public required string AppUserId { get; set; }
    }
}
