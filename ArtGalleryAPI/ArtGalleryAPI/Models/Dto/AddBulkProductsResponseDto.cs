namespace ArtGalleryAPI.Models.Dto
{
    public class AddBulkProductsResponseDto
    {
        public string Name {  get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public Guid Categoryid { get; set; }
        public string Status { get; set; }
        public string ErrorInfo { get; set; }
    }
}
