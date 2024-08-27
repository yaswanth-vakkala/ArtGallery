using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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

        [MaxLength(100, ErrorMessage = "User modified by can have a maximum of 10using Microsoft.AspNetCore.Identity;\r\nusing System.ComponentModel.DataAnnotations;\r\nusing System.ComponentModel.DataAnnotations.Schema;\r\n\r\nnamespace ArtGalleryAPI.Models.Domain\r\n{\r\n    public class AppUser : IdentityUser\r\n    {\r\n        [Required]\r\n        [MaxLength(50, ErrorMessage = \"User first name can have a maximum of 50 characters!\")]\r\n        public required string FirstName { get; set; }\r\n\r\n        [MaxLength(50, ErrorMessage = \"User last name can have a maximum of 50 characters!\")]\r\n        public string? LastName { get; set; }\r\n\r\n        [Required]\r\n        [MaxLength(30, ErrorMessage = \"User status can have a maximum of 30 characters!\")]\r\n        public required string Status { get; set; }\r\n\r\n        [MaxLength(6, ErrorMessage = \"User country code can have a maximum of 6 characters!\")]\r\n        public string? CountryCode { get; set; }\r\n\r\n        [Required]\r\n        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;\r\n\r\n        public DateTime? ModifiedAt { get; set; }\r\n\r\n        [MaxLength(100, ErrorMessage = \"User modified by can have a maximum of 100 characters!\")]\r\n        public string? ModifiedBy { get; set; }\r\n        public DateTime? LastLoginAt { get; set; }\r\n\r\n        public IEnumerable<Address> Addresses { get; set; }\r\n    }\r\n}\r\n0 characters!")]
        public string? ModifiedBy { get; set; }

        public DateTime? LastLoginAt { get; set; }
    }
}
