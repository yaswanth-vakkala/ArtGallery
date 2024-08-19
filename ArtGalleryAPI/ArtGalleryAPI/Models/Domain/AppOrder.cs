using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Domain
{
    public class AppOrder
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedAt { get; set; }

        [MaxLength(100, ErrorMessage = "Order modified by can have a maximum of 100 characters!")]
        public string? ModifiedBy { get; set; }
    }
}
