using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ArtGalleryAPI.Services.Implementation
{
    public class AddressService : IAddressInterface
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AddressService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            var addresses = await dbContext.Address.ToListAsync();
            return addresses;
        }
        public async Task<Address>? GetAddressByIdAsync(Guid addressId)
        {
            var userId = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid").FirstOrDefault().Value;
            var address = await dbContext.Address.SingleOrDefaultAsync(address => address.AddressId == addressId);
            if(userId != address.AppUserId)
            {
                return null;
            }
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
            var userId = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid").FirstOrDefault().Value;
            var address = await dbContext.Address.SingleOrDefaultAsync(address => address.AddressId == addressId);
            var isAdmin = httpContextAccessor.HttpContext.User.IsInRole("Writer");
            if (address == null || (userId != address.AppUserId && !isAdmin))
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
            var userId = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/primarysid").FirstOrDefault().Value;
            var address = await dbContext.Address.SingleOrDefaultAsync(address => address.AddressId == addressId);
            var isAdmin = httpContextAccessor.HttpContext.User.IsInRole("Writer");
            if (address == null || (userId != address.AppUserId && !isAdmin))
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
        public async Task<IEnumerable<Address>> GetAddressesByUserIdAsync(string userId)
        {
            var addresses = await dbContext.Address.Where(addresses => addresses.AppUserId == userId).ToListAsync();
            return addresses;
        }
    }
}
