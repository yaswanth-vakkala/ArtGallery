using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Domain
{
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Address line can have a maximum of 500 characters!")]
        public required string AddressLine { get; set; }

        [Required]
        [MaxLength(12, ErrorMessage = "Address pin code can have a maximum of 12 characters!")]
        public required string PinCode { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "City in address can have a maximum of 100 characters!")]
        public required string City { get; set; }

        [MaxLength(100, ErrorMessage = "Landmark in address can have a maximum of 100 characters!")]
        public string? Landmark { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Country in address can have a maximum of 100 characters!")]
        public required string Country { get; set; }

        [Required]
        [MaxLength(6, ErrorMessage = "Country code in address can have a maximum of 6 characters!")]
        public required string CountryCode { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "Phone number in address can have a maximum of 15 characters!")]
        public required string PhoneNumber { get; set; }

    }
}
