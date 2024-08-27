using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Implementation;
using ArtGalleryAPI.Services.Interface;
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
                    Quantity = newCart.Quantity,
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
        /// update a new cart item
        /// </summary>
        /// <param name="newCart"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{cartId:Guid}")]
        public async Task<IActionResult> UpdateCart([FromRoute] Guid cartId, [FromBody] UpdateCartDto updatedCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var result = await cartService.UpdateCartAsync(cartId, updatedCart);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
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

    }
}
