namespace ArtGalleryAPI.Models.Dto
{
    public class TopSellingProductDto
    {
        public Guid ProductId { get; set; } // ProductId is of type Guid
        public string Name { get; set; } // Product Name
        public decimal TotalSales { get; set; } // Total sales, assuming it's a decimal
    }

}
