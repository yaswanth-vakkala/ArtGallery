using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class OrderItemService : IOrderItemInterface
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderItemService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrdersItemsAsync()
        {
            var orderItems = await dbContext.OrderItem.ToListAsync();
            return orderItems;
        }

        public async Task<OrderItem>? GetOrderItemByIdAsync(Guid orderItemId)
        {
            var orderItem = await dbContext.OrderItem.SingleOrDefaultAsync(o => o.OrderItemId == orderItemId);
            var order = await dbContext.AppOrder.FirstOrDefaultAsync(x => x.OrderId == orderItem.OrderId);
            var isAdmin = httpContextAccessor.HttpContext.User.IsInRole("Writer");
            var userId = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid").FirstOrDefault().Value;
            if(!isAdmin && userId != order.AppUserId)
            {
                return null;
            }
            return orderItem;
        }
        
        public async Task<OrderItemsFullDto>? GetOrderItemFullByIdAsync(Guid orderItemId)
        {
            var orderItem = await dbContext.OrderItem.SingleOrDefaultAsync(o => o.OrderItemId == orderItemId);
            var product = await dbContext.Product.SingleOrDefaultAsync(p => p.ProductId == orderItem.ProductId);
            var order = await dbContext.AppOrder.FirstOrDefaultAsync(x => x.OrderId == orderItem.OrderId);
            var isAdmin = httpContextAccessor.HttpContext.User.IsInRole("Writer");
            var userId = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid").FirstOrDefault().Value;
            if (!isAdmin && userId != order.AppUserId)
            {
                return null;
            }
            return new OrderItemsFullDto()
            {
                OrderId = orderItem.OrderId,
                OrderItemId = orderItem.OrderItemId,
                ProductCost = orderItem.ProductCost,
                ShippingCost = orderItem.ShippingCost,
                TaxCost = orderItem.TaxCost,
                Product = product,
                Status = orderItem.Status,
            };
        }
        public async Task<OrderItem> CreateOrderItemAsync(OrderItem newOrderItem)
        {
            var order = await dbContext.AppOrder.FirstOrDefaultAsync(x => x.OrderId == newOrderItem.OrderId);
            var isAdmin = httpContextAccessor.HttpContext.User.IsInRole("Writer");
            var userId = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid").FirstOrDefault().Value;
            if(!isAdmin && userId != order.AppUserId)
            {
                return null;
            }
            await dbContext.OrderItem.AddAsync(newOrderItem);
            await dbContext.SaveChangesAsync();
            return newOrderItem;
        }

        public async Task<IEnumerable<OrderItem>> CreateOrderItemsAsync(IEnumerable<OrderItem> newOrderItems)
        {
            var userId = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid").FirstOrDefault().Value;
            var isAdmin = httpContextAccessor.HttpContext.User.IsInRole("Writer");
            foreach (var orderItem in newOrderItems) {
                var order = await dbContext.AppOrder.FirstOrDefaultAsync(x => x.OrderId == orderItem.OrderId);
                if (!isAdmin && userId != order.AppUserId)
                {
                    return null;
                }
                await dbContext.OrderItem.AddAsync(orderItem);
            }
            await dbContext.SaveChangesAsync();
            return newOrderItems;
        }
        public async Task<OrderItem>? UpdateOrderItemAsync(Guid orderItemId, UpdateOrderItemDto updatedOrderItem)
        {
            var orderItem = await dbContext.OrderItem.SingleOrDefaultAsync(o => o.OrderItemId == orderItemId);
            if (orderItem == null)
            {
                return null;
            }
            else
            {
                dbContext.Entry(orderItem).CurrentValues.SetValues(updatedOrderItem);
                await dbContext.SaveChangesAsync();
                return orderItem;
            }
        }

        public async Task<bool> DeleteOrderItemAsync(Guid orderItemId)
        {
            var orderItem = await dbContext.OrderItem.SingleOrDefaultAsync(o => o.OrderItemId == orderItemId);
            if (orderItem == null)
            {
                return false;
            }
            else
            {
                dbContext.OrderItem.Remove(orderItem);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
