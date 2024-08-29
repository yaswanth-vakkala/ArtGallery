using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Domain
{
    public class AppOrder
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedAt { get; set; }

        [MaxLength(100, ErrorMessage = "User modified by can have a maximum of 100 characters!")]
        public string? ModifiedBy { get; set; }

        [Required]
        public required Guid AddressId { get; set; }

        [Required]
        public required string AppUserId { get; set; }

        [Required]
        public required Guid PaymentId { get; set; }
    }
}
