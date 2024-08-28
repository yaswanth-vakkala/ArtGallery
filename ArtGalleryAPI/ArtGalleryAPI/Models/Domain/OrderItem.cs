using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtGalleryAPI.Models.Domain
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }

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

        [Required]
        public required Guid ProductId { get; set; }

        [Required]
        public required Guid OrderId { get; set; }
    }
}
