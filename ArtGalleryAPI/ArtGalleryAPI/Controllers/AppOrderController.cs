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
    public class AppOrderController : ControllerBase
    {
        private readonly IAppOrderInterface orderService;

        public AppOrderController(IAppOrderInterface orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// returns all the orders from the database
        /// </summary>
        /// <returns>list of all orders</returns>

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 8)
        {
            try
            {
                var orders = await orderService.GetAllOrdersAsync(pageNumber, pageSize);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// get count of the total orders
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [HttpGet]
        [Route("count/{userId:Guid}")]
        public async Task<IActionResult> GetOrderCount([FromRoute]string userId)
        {
            try
            {
                var orderCount = await orderService.GetOrderCountAsync(userId);
                return Ok(orderCount);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// returns all the orders items based on order id
        /// </summary>
        /// <returns>list of order items</returns>

        [HttpGet]
        [Route("orderItems/{orderId:Guid}")]
        public async Task<IActionResult> GetAllOrdersItemsByOrderId([FromRoute] Guid orderId)
        {
            try
            {
                var orderItems = await orderService.GetOrderItemsByOrderIdAsync(orderId);
                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// returns the filtered order record based on id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>filtered order</returns>
        [HttpGet]
        [Route("{orderId:Guid}")]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId)
        {
            try
            {
                var order = await orderService.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(order);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// returns the filtered order's record based on user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>filtered orders</returns>
        [HttpGet]
        [Route("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId([FromRoute] string userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 8)
        {
            try
            {
                var orders = await orderService.GetOrdersByUserIdAsync(userId, pageNumber, pageSize);
                if (orders == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(orders);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// add's a new order to db
        /// </summary>
        /// <param name="order"></param>
        /// <returns>new order</returns>
        [HttpPost]
        public async Task<IActionResult> Addorder([FromBody] AddAppOrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var neworder = new AppOrder
                {
                    AddressId = order.AddressId,
                    AppUserId = order.AppUserId,
                    PaymentId = order.PaymentId,
                    CreatedAt = DateTime.UtcNow,
                };
                await orderService.CreateOrderAsync(neworder);
                var locationUri = Url.Action("GetorderById", new { orderId = neworder.OrderId });
                return Created(locationUri, neworder);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// delete a order in db based on id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>bool representing state of operation</returns>
        [HttpDelete]
        [Route("{orderId:Guid}")]
        public async Task<IActionResult> Deleteorder([FromRoute] Guid orderId)
        {
            try
            {
                var deleteStatus = await orderService.DeleteOrderAsync(orderId);
                return Ok(deleteStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
