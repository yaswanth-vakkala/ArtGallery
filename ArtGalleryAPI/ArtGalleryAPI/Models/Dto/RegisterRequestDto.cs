using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class RegisterRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "User email can have a maximum of 100 characters!")]
        public required string Email { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "User password can have a maximum of 30 characters!")]
        public required string Password { get; set; }
    }
}
