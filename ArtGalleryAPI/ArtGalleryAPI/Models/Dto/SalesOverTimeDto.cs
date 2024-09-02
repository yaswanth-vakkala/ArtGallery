namespace ArtGalleryAPI.Models.Dto
{
    public class SalesOverTimeDto
    {
        public DateTime Date { get; set; } // Keep this as DateTime for internal processing
        public string FormattedDate => Date.ToString("yyyy-MM-dd"); // Format as needed
        public int OrderCount { get; set; }
    }
}
