namespace ArtGalleryAPI.Models.Dto
{
    public class SalesByCategoryDto
    {
        public int CategoryId { get; set; }
        public decimal TotalSales { get; set; } = 0;
    }
}
