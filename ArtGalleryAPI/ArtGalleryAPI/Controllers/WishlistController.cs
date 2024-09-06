using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Implementation;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistInterface wishlistService;

        public WishlistController(IWishlistInterface wishlistService)
        {
            this.wishlistService = wishlistService;
        }

       
        [HttpGet]
        [Route("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetAllWishlistsForUser([FromRoute] string userId)
        {
            try
            {
                var wishlists = await wishlistService.GetAllWishlistsForUserAsync(userId);
                return Ok(wishlists);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateWishlist(AddWishlistDto newWishlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var wishlist = new WishList()
                {
                    AppUserId = newWishlist.AppUserId,
                    ProductId = newWishlist.ProductId,
                    CreatedAt = DateTime.UtcNow,
                };
                await wishlistService.CreateWishlistAsync(wishlist);
                return Ok(wishlist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


       
        [HttpDelete]
        [Route("{cartId:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteWishlist([FromRoute] Guid wishlistId)
        {
            try
            {
                var deleteStatus = await wishlistService.DeleteWishlistAsync(wishlistId);
                return Ok(deleteStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [HttpPost]
        [Route("deleteWishlists")]
        [Authorize]
        public async Task<IActionResult> DeleteWishlists([FromBody] Guid[] wishlistIds)
        {
            try
            {
                var deleteStatus = await wishlistService.DeleteWishlistsAsync(wishlistIds);
                return Ok(deleteStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
