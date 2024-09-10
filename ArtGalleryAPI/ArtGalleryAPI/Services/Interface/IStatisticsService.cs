using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IStatisticsService
    {
        Task<int> GetTotalSalesAsync();
        Task<Dictionary<string, int>> GetCategoryOrderCountsAsync();
        Task<Dictionary<string, Dictionary<string, int>>> GetOrdersByCustomerIdMonthWiseAsync(string customerId);
        Task<Dictionary<int, Dictionary<int, int>>> GetMonthlySalesByYearAsync();
        Task<int> GetTotalProductsSoldAsync();
        Task<List<TopSellingProductDto>> GetTopSellingProductsAsync(int topN);
        Task<List<SalesOverTimeDto>> GetOrdersInDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
