using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class AddOrderItemDto
    {
        [Required]
        public required int Quantity { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Order item status can have a maximum of 30 characters!")]
        public required string Status { get; set; }

        [Required]
        [Precision(13, 3)]
        public required decimal ProductCost { get; set; }

        [Required]
        [Precision(13, 3)]
        public required decimal TaxCost { get; set; }

        [Required]
        [Precision(13, 3)]
        public required decimal ShippingCost { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }
    }
}
