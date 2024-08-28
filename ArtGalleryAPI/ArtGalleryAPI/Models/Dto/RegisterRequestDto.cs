using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Please enter a valid email")]
        [MaxLength(100, ErrorMessage = "User email can have a maximum of 100 characters!")]
        public required string Email { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "User password can have a maximum of 30 characters!")]
        public required string Password { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "User first name can have a maximum of 50 characters!")]
        public required string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "User last name can have a maximum of 50 characters!")]
        public string? LastName { get; set; }

        [MaxLength(6, ErrorMessage = "User country code can have a maximum of 6 characters!")]
        public string? CountryCode { get; set; }

        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Please enter a valid phone number")]
        [MaxLength(15, ErrorMessage = "User phone number can have a maximum of 15 characters")]
        public string? PhoneNumber { get; set; }

        public bool isAdmin { get; set; }
    }
}
