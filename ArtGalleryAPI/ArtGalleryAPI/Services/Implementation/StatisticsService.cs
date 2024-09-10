using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext dbContext;

        public StatisticsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get the total sales amount from all payments
        public async Task<int> GetTotalSalesAsync()
        {
            return await dbContext.AppOrder.CountAsync();
        }

        // Get sales grouped by category
        public async Task<Dictionary<string, int>> GetCategoryOrderCountsAsync()
        {
            var categoryOrderCounts = await dbContext.OrderItem
                .Join(dbContext.Product,
                      orderItem => orderItem.ProductId,
                      product => product.ProductId,
                      (orderItem, product) => new { orderItem, product })
                .Join(dbContext.Category,
                      combined => combined.product.CategoryId,
                      category => category.CategoryId,
                      (combined, category) => new { combined.orderItem, combined.product, category })
                .GroupBy(result => result.category.Name)
                .Select(group => new
                {
                    CategoryName = group.Key,
                    OrderCount = group.Count()
                })
                .ToDictionaryAsync(x => x.CategoryName, x => x.OrderCount);

            return categoryOrderCounts;
        }

        public async Task<Dictionary<string, Dictionary<string, int>>> GetOrdersByCustomerIdMonthWiseAsync(string customerId)
        {
                var orderCountsByMonth = await dbContext.AppOrder
                    .Where(o => o.AppUserId == customerId)
                    .GroupBy(o => new { o.CreatedAt.Year, o.CreatedAt.Month })
                    .OrderBy(g => g.Key.Year)
                    .ThenBy(g => g.Key.Month)
                    .ToDictionaryAsync(
                        g => $"{g.Key.Year}-{g.Key.Month:D2}", // Key as "YYYY-MM"
                        g => g.Count()
                    );

                // Return a dictionary where the key is the customerId
                return new Dictionary<string, Dictionary<string, int>>
        {
            { customerId, orderCountsByMonth }
        };
        }

        // Get sales grouped by month
        public async Task<Dictionary<int, Dictionary<int, int>>> GetMonthlySalesByYearAsync()
        {
            var salesData = await dbContext.AppOrder
                .GroupBy(order => new { order.CreatedAt.Year, order.CreatedAt.Month })
                .Select(group => new
                {
                    Year = group.Key.Year,
                    Month = group.Key.Month,
                    SalesCount = group.Count()
                })
                .ToListAsync();

            var salesByYear = salesData
                .GroupBy(s => s.Year)
                .ToDictionary(
                    yearGroup => yearGroup.Key,
                    yearGroup => yearGroup
                        .ToDictionary(
                            monthGroup => monthGroup.Month,
                            monthGroup => monthGroup.SalesCount
                        )
                );

            return salesByYear;
        }

        // Get the total quantity of products sold
        public async Task<int> GetTotalProductsSoldAsync()
        {
            return await dbContext.OrderItem.CountAsync(); // Count the total number of OrderItem entries
        }

        // Get the top N selling products
        // Get the top N selling products based on quantity sold
        public async Task<List<TopSellingProductDto>> GetTopSellingProductsAsync(int topN)
        {
            return await dbContext.OrderItem
                .Join(dbContext.Product,
                      oi => oi.ProductId, // Foreign key in OrderItem
                      p => p.ProductId,
                      (oi, p) => new { OrderItem = oi, Product = p }) // Create an anonymous object
                .GroupBy(x => new { x.Product.ProductId, x.Product.Name }) // Group by Product Id and Name
                .Select(g => new TopSellingProductDto
                {
                    ProductId = g.Key.ProductId, // Use the Id from Product
                    Name = g.Key.Name,
                    TotalSales = g.Count() // Count of OrderItems for that product
                })
                .OrderByDescending(x => x.TotalSales) // Order by TotalSales descending
                .Take(topN) // Limit to top N products
                .ToListAsync(); // Convert to List
        }





        // Get sales totals over a specified time range
        public async Task<List<SalesOverTimeDto>> GetOrdersInDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await dbContext.AppOrder
                .Where(o => o.CreatedAt >= startDate && o.CreatedAt <= endDate.AddDays(1)) // Filter by date range
                .GroupBy(o => o.CreatedAt.Date) // Group by date
                .Select(g => new SalesOverTimeDto
                {
                    Date = g.Key, // Date
                    OrderCount = g.Count() // Count of orders for that date
                })
                .ToListAsync(); // Convert to List
        }


    }
}
