using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Domain
{
    public class AppUser
    {
        [Key]
        public Guid AppUserId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "User first name can have a maximum of 50 characters!")]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "User last name can have a maximum of 50 characters!")]
        public required string LastName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "User email can have a maximum of 100 characters!")]
        public required string Email { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "User role can have a maximum of 30 characters!")]
        public required string Role { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "User status can have a maximum of 30 characters!")]
        public required string Status { get; set; }

        [MaxLength(6, ErrorMessage = "User country code can have a maximum of 6 characters!")]
        public required string CountryCode { get; set; }

        [MaxLength(15, ErrorMessage = "User phone number can have a maximum of 15 characters!")]
        public required string PhoneNumber { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedAt { get; set; }

        [MaxLength(100, ErrorMessage = "User modified by can have a maximum of 100 characters!")]
        public string? ModifiedBy { get; set; }
    }
}
