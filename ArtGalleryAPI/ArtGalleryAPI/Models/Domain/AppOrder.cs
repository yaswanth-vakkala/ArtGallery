using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Domain
{
    public class AppOrder
    {
        [Key]
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(Address))]
        public Guid AddressId { get; set; }
        public Address Address { get; set; }

        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        [MaxLength(100, ErrorMessage = "User modified by can have a maximum of 100 characters!")]
        public string? ModifiedBy { get; set; } = String.Empty;
    }
}
