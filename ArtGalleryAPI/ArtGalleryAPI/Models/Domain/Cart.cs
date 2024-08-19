using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Domain
{
    public class Cart
    {
        [Key]
        public Guid CartId { get; set; }

        [Required]
        public required int Quantity { get; set; }
    }
}
