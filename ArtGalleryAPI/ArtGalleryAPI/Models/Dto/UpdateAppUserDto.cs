using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class UpdateAppUserDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "User first name can have a maximum of 50 characters!")]
        public required string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "User last name can have a maximum of 50 characters!")]
        public string? LastName { get; set; }

        [MaxLength(6, ErrorMessage = "User country code can have a maximum of 6 characters!")]
        public string? CountryCode { get; set; }

        [MaxLength(15, ErrorMessage = "User phone number can have a maximum of 15 characters")]
        public string? PhoneNumber { get; set; }
    }
}
