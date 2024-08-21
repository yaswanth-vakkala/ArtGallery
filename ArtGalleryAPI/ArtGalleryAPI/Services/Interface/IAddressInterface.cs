using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public class IAddressInterface
    {
        Task<IEnumerable<Address>> GetAllAddressesAsync();
        Task<Address>? GetAddressByIdAsync(Guid addressId);
        Task<Address> AddAddressAsync(Address newAddress);
        Task<Product>? UpdateAddressAsync(UpdateAddressDto updatedAddress);
        Task<bool> DeleteAddressAsync(Guid addressId);
    }
}
