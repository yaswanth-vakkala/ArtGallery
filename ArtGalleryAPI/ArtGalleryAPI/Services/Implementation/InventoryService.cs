using ArtGalleryAPI.CustomExceptions;
using ArtGalleryAPI.Data;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Services.Implementation
{
    public class InventoryService:IInventoryInterface
    {
        private readonly ApplicationDbContext dbContext;
        public InventoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Inventory>> GetAllInventoryAsync()
        {
            var inventory = await dbContext.Inventory.ToListAsync();
            return inventory;
        }
        public async Task<Inventory>? GetInventoryByIdAsync(Guid inventoryId)
        {
            var inventory = await dbContext.Inventory.SingleOrDefaultAsync(inventory => inventory.InventoryId == inventoryId);
            return inventory;
        }
        public async Task<Inventory> CreateInventoryAsync(Inventory newInventory)
        {
            await dbContext.Inventory.AddAsync(newInventory);
            await dbContext.SaveChangesAsync();
            return newInventory;
        }
        public async Task<Inventory>? UpdateInventoryAsync(Guid inventoryId,UpdateInventoryDto updatedInventory)
        {
            var inventory = await dbContext.Inventory.SingleOrDefaultAsync(inventory => inventory.InventoryId == inventoryId);
            if (inventory == null)
            {
                return null;
            }
            else
            {
                dbContext.Entry(inventory).CurrentValues.SetValues(updatedInventory);
                await dbContext.SaveChangesAsync();
                return inventory;
            }
        }
        public async Task<bool> DeleteInventoryAsync(Guid inventoryId)
        {
            var inventory = await dbContext.Inventory.SingleOrDefaultAsync(inventory => inventory.InventoryId == inventoryId);
            if (inventory == null)
            {
                return false;
            }
            else
            {
                dbContext.Inventory.Remove(inventory);
                await dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
