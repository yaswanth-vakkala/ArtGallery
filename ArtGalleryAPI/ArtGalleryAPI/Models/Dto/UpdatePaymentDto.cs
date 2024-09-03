using ArtGalleryAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class UpdatePaymentDto
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Payment status can have a maximum of 30 characters")]
        public required string Status { get; set; }

        [Required]
        [Precision(13, 3)]
        public required decimal Amount { get; set; }

        public DateTime? ModifiedAt { get; set; }


        [Required]
        [MaxLength(100, ErrorMessage = "Payment method can have a maximum of 100 characters!")]
        public required string PaymentMethod { get; set; }



    }
}
