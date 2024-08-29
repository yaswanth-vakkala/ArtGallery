using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class LoginResponseDto
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "User email can have a maximum of 100 characters!")]

        public required string Email { get; set; }
        [Required]
        public required string Token { get; set; }
        [Required]
        public required List<string> Roles { get; set; }
    }
}
