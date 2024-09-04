using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Domain
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; }

        [Required]
        [Precision(13, 3)]
        public required decimal Amount { get; set; }

        [Required]
        public required DateTime PaymentDate { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Card Number can have a maximum of 100 characters!")]
        public required string CardNumber { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Card Holder name can have a maximum of 100 characters!")]
        public required string CardHolderName { get; set; }

        [Required]
        public required DateOnly ExpiryDate { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Payment status can have a maximum of 30 characters")]
        public required string Status { get; set; }
    }
}
