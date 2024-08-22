using Microsoft.AspNetCore.Identity;

namespace ArtGalleryAPI.Services.Interface
{
    public interface ITokenInterface
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
