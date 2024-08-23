using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;

namespace ArtGalleryAPI.Services.Interface
{
    public interface IInventoryInterface
    {
        Task<IEnumerable<Inventory>> GetAllInventoryAsync();
        Task<Inventory>? GetInventoryByIdAsync(Guid inventoryId);
        Task<Inventory> CreateInventoryAsync(Inventory newInventory);
        Task<Inventory>? UpdateInventoryAsync(Guid inventoryId,UpdateInventoryDto updatedInventory);
        Task<bool> DeleteInventoryAsync(Guid inventoryId);

    }
}
