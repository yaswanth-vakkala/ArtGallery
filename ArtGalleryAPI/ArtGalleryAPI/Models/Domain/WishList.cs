using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models.Domain
{
    public class WishList
    {
        [Key]
        public Guid WishListId { get; set; }


    }
}
