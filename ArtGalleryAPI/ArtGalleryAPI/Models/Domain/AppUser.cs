using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ArtGalleryAPI.Models.Domain
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(50, ErrorMessage = "User first name can have a maximum of 50 characters!")]
        public required string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "User last name can have a maximum of 50 characters!")]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "User status can have a maximum of 30 characters!")]
        public required string Status { get; set; }

        [MaxLength(6, ErrorMessage = "User country code can have a maximum of 6 characters!")]
        public string? CountryCode { get; set; }

        [Required]
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedAt { get; set; }

        [MaxLength(100, ErrorMessage = "User modified by can have a maximum of 100 characters!")]
        public string? ModifiedBy { get; set; }

        public DateTime? LastLoginAt { get; set; }
    }
}
