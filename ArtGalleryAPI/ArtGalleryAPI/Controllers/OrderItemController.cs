using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Implementation;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemInterface orderItemService;

        public OrderItemController(IOrderItemInterface orderItemService)
        {
            this.orderItemService = orderItemService;
        }

        /// <summary>
        /// returns all the orderItems from the database
        /// </summary>
        /// <returns>list of all orderItems</returns>

        [HttpGet]
        public async Task<IActionResult> GetAllorderItems()
        {
            try
            {
                var orderItems = await orderItemService.GetAllOrdersItemsAsync();
                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// returns the filtered orderItem record based on id
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns>filtered orderItem</returns>
        [HttpGet]
        [Route("{orderItemId:Guid}")]
        public async Task<IActionResult> GetorderItemById([FromRoute] Guid orderItemId)
        {
            try
            {
                var orderItem = await orderItemService.GetOrderItemByIdAsync(orderItemId);
                if (orderItem == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(orderItem);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// add's a new orderItem to db
        /// </summary>
        /// <param name="orderItem"></param>
        /// <returns>new orderItem</returns>
        [HttpPost]
        public async Task<IActionResult> AddorderItem([FromBody] AddOrderItemDto orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var neworderItem = new OrderItem
                {
                    Quantity = orderItem.Quantity,
                    Status = orderItem.Status,
                    ProductCost = orderItem.ProductCost,
                    TaxCost = orderItem.TaxCost,
                    ShippingCost = orderItem.ShippingCost,
                    ProductId = orderItem.ProductId,
                    OrderId = orderItem.OrderId,
                };
                await orderItemService.CreateOrderItemAsync(neworderItem);
                var locationUri = Url.Action("GetorderItemById", new { orderItemId = neworderItem.OrderItemId });
                return Created(locationUri, neworderItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// updates the existing orderItem in db
        /// </summary>
        /// <param name="updatedorderItem"></param>
        /// <returns>updated orderItem</returns>
        [HttpPut]
        [Route("{orderItemId:Guid}")]
        public async Task<IActionResult> UpdateorderItem([FromRoute] Guid orderItemId, [FromBody] UpdateOrderItemDto updatedorderItem)
        {
            try
            {
                var result = await orderItemService.UpdateOrderItemAsync(orderItemId, updatedorderItem);
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
        /// delete a orderItem in db based on id
        /// </summary>
        /// <param name="orderItemId"></param>
        /// <returns>bool representing state of operation</returns>
        [HttpDelete]
        [Route("{orderItemId:Guid}")]
        public async Task<IActionResult> DeleteorderItem([FromRoute] Guid orderItemId)
        {
            try
            {
                var deleteStatus = await orderItemService.DeleteOrderItemAsync(orderItemId);
                return Ok(deleteStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
