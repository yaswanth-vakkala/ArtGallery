using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class UpdateCartDto
    {
        [Required]
        public required int Quantity { get; set; }
    }
}
