using ArtGalleryAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class AppOrdersFullDto
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public required Guid AddressId { get; set; }

        [Required]
        public required string AppUserId { get; set; }

        [Required]
        public required Guid PaymentId { get; set; }

        public IEnumerable<OrderItemsFullDto>? OrderItems { get; set; }
    }
}
