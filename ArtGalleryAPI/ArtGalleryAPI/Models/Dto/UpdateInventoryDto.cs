using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class UpdateInventoryDto
    {
        [Required]
        public required int Quantity { get; set; }

    }
}
