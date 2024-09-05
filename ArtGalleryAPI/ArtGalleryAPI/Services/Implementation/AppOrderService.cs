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
        public async Task<IEnumerable<AppOrdersFullDto>> GetAllOrdersAsync(int pageNumber, int pageSize, string? query = null, string? sortBy = null, string? sortOrder = null)
        {
            var skipResults = (pageNumber - 1) * pageSize;
            List<AppOrdersFullDto> appOrdersFull = new List<AppOrdersFullDto>();
            var orders = await dbContext.AppOrder.ToListAsync();
            if (string.IsNullOrWhiteSpace(query) == false)
            {
                orders = await dbContext.AppOrder.Where(o => o.AppUserId.Contains(query) || o.OrderId == new Guid(query) || o.AddressId == new Guid(query)).ToListAsync();
            }

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (string.Equals(sortBy, "date", StringComparison.OrdinalIgnoreCase))
                {
                    var isDesc = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase) ? true : false;
                    orders = isDesc ? orders.OrderByDescending(o => o.CreatedAt).ToList() : orders.OrderBy(o => o.CreatedAt).ToList();
                }
            }
            foreach (var order in orders)
            {
                var orderItems = await dbContext.OrderItem.Where(o => o.OrderId == order.OrderId).ToListAsync();
                List<OrderItemsFullDto> orderItemsFulls = new List<OrderItemsFullDto>();
                foreach (var orderItem in orderItems)
                {
                    var product = await dbContext.Product.SingleOrDefaultAsync(p => p.ProductId == orderItem.ProductId);
                    orderItemsFulls.Add(new OrderItemsFullDto
                    {
                        OrderItemId = orderItem.OrderItemId,
                        Status = orderItem.Status,
                        ProductCost = orderItem.ProductCost,
                        ShippingCost = orderItem.ShippingCost,
                        TaxCost = orderItem.TaxCost,
                        OrderId = orderItem.OrderId,
                        Product = product
                    });
                }
                appOrdersFull.Add(new AppOrdersFullDto()
                {
                    AddressId = order.AddressId,
                    PaymentId = order.PaymentId,
                    AppUserId = order.AppUserId,
                    CreatedAt = order.CreatedAt,
                    OrderId = order.OrderId,
                    OrderItems = orderItemsFulls
                });
            }
            var result = appOrdersFull.Skip(skipResults).Take(pageSize);
            return result.ToList();
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

        public async Task<int> GetOrderCountAsync(string userId)
        {
            var orderCount = await dbContext.AppOrder.Where(o => o.AppUserId == userId).CountAsync();
            return orderCount;
        }

        public async Task<int> GetOrdersCountAsync(string? query)
        {
            var orderCount = 0;
            if (!string.IsNullOrWhiteSpace(query))
            {
                orderCount = await dbContext.AppOrder.Where(o => o.AppUserId.Contains(query) || o.OrderId == new Guid(query) || o.AddressId == new Guid(query)).CountAsync();

            }
            else
            {
                orderCount = await dbContext.AppOrder.CountAsync();
            }
            return orderCount;
        }

        public async Task<IEnumerable<AppOrdersFullDto>>? GetOrdersByUserIdAsync(string userId, int pageNumber, int pageSize)
        {
            var skipResults = (pageNumber - 1) * pageSize;
            List<AppOrdersFullDto> appOrdersFull = new List<AppOrdersFullDto>();
            var orders = await dbContext.AppOrder.Where(o => o.AppUserId == userId).OrderByDescending(o => o.CreatedAt).ToListAsync();
            foreach (var order in orders)
            {
                var orderItems = await dbContext.OrderItem.Where(o => o.OrderId == order.OrderId).ToListAsync();
                List<OrderItemsFullDto> orderItemsFulls = new List<OrderItemsFullDto>();
                foreach (var orderItem in orderItems)
                {
                    var product = await dbContext.Product.SingleOrDefaultAsync(p => p.ProductId == orderItem.ProductId);
                    orderItemsFulls.Add(new OrderItemsFullDto
                    {
                        OrderItemId = orderItem.OrderItemId,
                        Status = orderItem.Status,
                        ProductCost = orderItem.ProductCost,
                        ShippingCost = orderItem.ShippingCost,
                        TaxCost = orderItem.TaxCost,
                        OrderId = orderItem.OrderId,
                        Product = product
                    });
                }
                appOrdersFull.Add(new AppOrdersFullDto()
                {
                    AddressId = order.AddressId,
                    PaymentId = order.PaymentId,
                    AppUserId = order.AppUserId,
                    CreatedAt = order.CreatedAt,
                    OrderId = order.OrderId,
                    OrderItems = orderItemsFulls
                });
            }
            var result = appOrdersFull.Skip(skipResults).Take(pageSize);
            return result.ToList();
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
