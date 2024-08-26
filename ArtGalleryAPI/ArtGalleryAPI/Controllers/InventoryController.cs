using ArtGalleryAPI.CustomExceptions;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Implementation;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController:ControllerBase
    {
        private readonly IInventoryInterface inventoryService;
        public InventoryController(IInventoryInterface inventoryService)
        {
            this.inventoryService = inventoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllInventory()
        {
            try
            {
                var inventory = await inventoryService.GetAllInventoryAsync();
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{inventoryId:Guid}")]
        public async Task<IActionResult> GetInventoryById([FromRoute] Guid inventoryId)
        {
            try
            {
                var inventory = await inventoryService.GetInventoryByIdAsync(inventoryId);
                if (inventory == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(inventory);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateInventory([FromBody] InventoryDto inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var newInventory = new Inventory
                {
                    Quantity = inventory.Quantity,
                    CreatedAt = DateTime.UtcNow,
                };
                await inventoryService.CreateInventoryAsync(newInventory);
                var locationUri = Url.Action("GetInventoryById", new { inventoryId = newInventory.InventoryId });
                return Created(locationUri, newInventory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("{inventoryId:Guid}")]
        public async Task<IActionResult> UpdateInventory([FromRoute] Guid inventoryId,[FromBody] UpdateInventoryDto updatedInventory)
        {
            try
            {
                var result = await inventoryService.UpdateInventoryAsync(inventoryId,updatedInventory);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("{inventoryId:Guid}")]
        public async Task<IActionResult> DeleteInventory([FromRoute] Guid inventoryId)
        {
            try
            {
                var deleteStatus = await inventoryService.DeleteInventoryAsync(inventoryId);
                return Ok(deleteStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
