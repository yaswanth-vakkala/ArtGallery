using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Domain
{
    public class Inventory
    {
        [Key]
        public Guid InventoryId { get; set; }

        [Required]
        public required int Quantity { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedAt { get; set; }

        [MaxLength(100, ErrorMessage = "Inventory modified by can have a maximum of 100 characters!")]
        public string? ModifiedBy { get; set; }
    }
}
