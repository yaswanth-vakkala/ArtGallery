using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class UpdatePaymentDto
    {
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Payment Method can have a maximum of 100 characters!")]
        public required string PaymentMethod { get; set; }


        [Required]
        [Precision(13, 2)]
        public decimal Amount { get; set; }
    }
}
