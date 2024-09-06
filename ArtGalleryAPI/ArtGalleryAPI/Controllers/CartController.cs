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
    public class CartController : ControllerBase
    {
        private readonly ICartInterface cartService;

        public CartController(ICartInterface cartService)
        {
            this.cartService = cartService;
        }

        /// <summary>
        /// Get all cart items for an user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetAllCartsForUser([FromRoute] string userId)
        {
            try
            {
                var carts = await cartService.GetAllCartsForUserAsync(userId);
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// create a new cart item
        /// </summary>
        /// <param name="newCart"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCart(AddCartDto newCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var cart = new Cart()
                {
                    AppUserId = newCart.AppUserId,
                    ProductId = newCart.ProductId,
                    CreatedAt = DateTime.UtcNow,
                };
                await cartService.CreateCartAsync(cart); ;
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// delete a cart in db based on id
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns>bool representing state of operation</returns>
        [HttpDelete]
        [Route("{cartId:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteCart([FromRoute] Guid cartId)
        {
            try
            {
                var deleteStatus = await cartService.DeleteCartAsync(cartId);
                return Ok(deleteStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// delete a carts in db based on product ids
        /// </summary>
        /// <param name="cartIds"></param>
        /// <returns>bool representing state of operation</returns>
        [HttpPost]
        [Route("deleteCarts")]
        [Authorize]
        public async Task<IActionResult> DeleteCarts([FromBody] Guid[] cartIds)
        {
            try
            {
                var deleteStatus = await cartService.DeleteCartsAsync(cartIds);
                return Ok(deleteStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
