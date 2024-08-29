using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Dto
{
    public class ProductsIdListRequest
    {
        [Required]
        public required IEnumerable<Guid> productIds { get; set; }
    }
}
