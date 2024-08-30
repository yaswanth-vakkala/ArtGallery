using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class AppOrderService : IAppOrderInterface
    {
        private readonly ApplicationDbContext dbContext;

        public AppOrderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<AppOrder>> GetAllOrdersAsync()
        {
            var orders = await dbContext.AppOrder.ToListAsync();
            return orders;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId)
        {
            var orderItems = await dbContext.OrderItem.Where(o => o.OrderId == orderId).ToListAsync();
            return orderItems;
        }
        public async Task<AppOrder>? GetOrderByIdAsync(Guid orderId)
        {
            var order = await dbContext.AppOrder.SingleOrDefaultAsync(o => o.OrderId == orderId);
            return order;
        }

        public async Task<IEnumerable<AppOrdersFullDto>>? GetOrdersByUserIdAsync(string userId)
        {
            List<AppOrdersFullDto> appOrdersFull = new List<AppOrdersFullDto>();
            var orders = await dbContext.AppOrder.Where(o => o.AppUserId == userId).ToListAsync();
            foreach (var order in orders) {
                var orderItems = await dbContext.OrderItem.Where(o => o.OrderId == order.OrderId).ToListAsync();
                appOrdersFull.Add(new AppOrdersFullDto()
                {
                    AddressId = order.AddressId,
                    PaymentId = order.PaymentId,
                    AppUserId = order.AppUserId,
                    CreatedAt = order.CreatedAt,
                    OrderId = order.OrderId,
                    OrderItems = orderItems
                });
            }
            return appOrdersFull;
        }
        public async Task<AppOrder> CreateOrderAsync(AppOrder newOrder)
        {
            await dbContext.AppOrder.AddAsync(newOrder);
            await dbContext.SaveChangesAsync();
            return newOrder;
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await dbContext.AppOrder.SingleOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                return false;
            }
            else
            {
                dbContext.AppOrder.Remove(order);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
