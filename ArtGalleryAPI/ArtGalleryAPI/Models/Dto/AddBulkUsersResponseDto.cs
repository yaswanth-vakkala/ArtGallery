using ArtGalleryAPI.Models.Domain;

namespace ArtGalleryAPI.Models.Dto
{
    public class AddBulkUsersResponseDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IsAdmin { get; set; }
        public string status { get; set; }
        public string ErrorInfo { get; set; }

    }
}
