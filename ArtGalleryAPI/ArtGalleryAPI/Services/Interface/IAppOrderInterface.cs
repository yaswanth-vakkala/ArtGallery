using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IAppOrderInterface
    {
        Task<IEnumerable<AppOrdersFullDto>> GetAllOrdersAsync(int pageNumber, int pageSize);
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid orderId);
        Task<AppOrder>? GetOrderByIdAsync(Guid orderId);
        Task<int>? GetOrderCountAsync(string userId);
        Task<IEnumerable<AppOrdersFullDto>>? GetOrdersByUserIdAsync(string userId, int pageNumber, int pageSize);
        Task<AppOrder> CreateOrderAsync(AppOrder newOrder);
        Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
