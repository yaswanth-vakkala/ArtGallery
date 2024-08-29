using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IAddressInterface
    {
        Task<IEnumerable<Address>> GetAllAddressesAsync();
        Task<Address>? GetAddressByIdAsync(Guid addressId);
        Task<Address> AddAddressAsync(Address newAddress);
        Task<Address>? UpdateAddressAsync(Guid addressId, UpdateAddressDto updatedAddress);
        Task<bool> DeleteAddressAsync(Guid addressId);
    }
}
