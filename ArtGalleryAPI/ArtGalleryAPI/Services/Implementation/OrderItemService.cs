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

        public OrderItemService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrdersItemsAsync()
        {
            var orderItems = await dbContext.OrderItem.ToListAsync();
            return orderItems;
        }

        public async Task<OrderItem>? GetOrderItemByIdAsync(Guid orderItemId)
        {
            var orderItem = await dbContext.OrderItem.SingleOrDefaultAsync(o => o.OrderItemId == orderItemId);
            return orderItem;
        }
        public async Task<OrderItem> CreateOrderItemAsync(OrderItem newOrderItem)
        {
            await dbContext.OrderItem.AddAsync(newOrderItem);
            await dbContext.SaveChangesAsync();
            return newOrderItem;
        }

        public async Task<IEnumerable<OrderItem>> CreateOrderItemsAsync(IEnumerable<OrderItem> newOrderItems)
        {
            foreach (var orderItem in newOrderItems) {
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
