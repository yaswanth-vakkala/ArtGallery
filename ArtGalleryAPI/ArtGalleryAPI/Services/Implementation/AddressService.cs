using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class AddressService : IAddressInterface
    {
        private readonly ApplicationDbContext dbContext;

        public AddressService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            var addresses = await dbContext.Address.ToListAsync();
            return addresses;
        }
        public async Task<Address>? GetAddressByIdAsync(Guid addressId)
        {
            var address = await dbContext.Address.SingleOrDefaultAsync(address => address.AddressId == addressId);
            return address;
        }
        public async Task<Address> AddAddressAsync(Address newAddress)
        {
            await dbContext.Address.AddAsync(newAddress);
            await dbContext.SaveChangesAsync();
            return newAddress;
        }
        public async Task<Address>? UpdateAddressAsync(Guid addressId, UpdateAddressDto updatedAddress)
        {
            var address = await dbContext.Address.SingleOrDefaultAsync(address => address.AddressId == addressId);
            if (address == null)
            {
                return null;
            }
            else
            {
                dbContext.Entry(address).CurrentValues.SetValues(updatedAddress);
                await dbContext.SaveChangesAsync();
                return address;
            }
        }
        public async Task<bool> DeleteAddressAsync(Guid addressId)
        {
            var address = await dbContext.Address.SingleOrDefaultAsync(address => address.AddressId == addressId);
            if (address == null)
            {
                return false;
            }
            else
            {
                dbContext.Address.Remove(address);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
