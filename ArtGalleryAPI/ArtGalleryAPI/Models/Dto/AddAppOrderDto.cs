using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class AddAppOrderDto
    {
        [Required]
        public required Guid AddressId { get; set; }
        [Required]
        public required Guid PaymentId { get; set; }
        [Required]
        public required string AppUserId { get; set; }


    }
}
