using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Writer")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet("total-sales")]
        public async Task<ActionResult<int>> GetTotalSales()
        {
            var totalSalesCount = await statisticsService.GetTotalSalesAsync();
            return Ok(totalSalesCount);
        }

        [HttpGet("category-order-counts")]
        public async Task<ActionResult<Dictionary<string, int>>> GetCategoryOrderCounts()
        {
            var categoryOrderCounts = await statisticsService.GetCategoryOrderCountsAsync();
            return Ok(categoryOrderCounts);
        }

        [HttpGet("customer-orders-monthwise")]
        public async Task<ActionResult<Dictionary<string, Dictionary<string, int>>>> GetOrdersByCustomerIdMonthWise(string customerId)
        {
            var orderCountsByMonth=await statisticsService.GetOrdersByCustomerIdMonthWiseAsync(customerId);
            return Ok(orderCountsByMonth);
        }

        [HttpGet("monthly-sales")]
        public async Task<ActionResult<Dictionary<int, Dictionary<int, int>>>> GetMonthlySales()
        {
            var monthlySales = await statisticsService.GetMonthlySalesByYearAsync();
            return Ok(monthlySales);
        }

        [HttpGet("total-products-sold")]
        public async Task<ActionResult<int>> GetTotalProductsSold()
        {
            var totalProductsSold = await statisticsService.GetTotalProductsSoldAsync();
            return Ok(totalProductsSold);
        }

        [HttpGet("top-selling-products")]
        public async Task<ActionResult<List<TopSellingProductDto>>> GetTopSellingProducts(int topN)
        {
            var topSellingProducts = await statisticsService.GetTopSellingProductsAsync(topN);
            return Ok(topSellingProducts);
        }

        [HttpGet("orders-in-date-range")]
        public async Task<ActionResult<List<SalesOverTimeDto>>> GetOrdersInDateRange(DateTime startDate, DateTime endDate)
        {
            var orders = await statisticsService.GetOrdersInDateRangeAsync(startDate, endDate);
            return Ok(orders);
        }
    }
}
