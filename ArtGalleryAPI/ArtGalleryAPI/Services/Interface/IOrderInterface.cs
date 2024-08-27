using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IOrderInterface
    {
        Task<IEnumerable<AppOrder>> GetAllOrdersAsync();
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId);
        Task<AppOrder>? GetOrderByIdAsync(Guid orderId);
        Task<AppOrder> CreateOrderAsync(AppOrder newOrder);
        Task<AppOrder>? UpdateOrderAsync(Guid orderId, UpdateAppOrderDto updatedOrder);
        Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
