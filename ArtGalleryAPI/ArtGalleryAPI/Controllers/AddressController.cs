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
    public class AddressController : ControllerBase
    {
        private readonly IAddressInterface addressService;
        public AddressController(IAddressInterface addressService)
        {
            this.addressService = addressService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAddresses()
        {
            try
            {
                var addresses = await addressService.GetAllAddressesAsync();
                return Ok(addresses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("{addressId:Guid}")]
        public async Task<IActionResult> GetAddressById([FromRoute] Guid addressId)
        {
            try
            {
                var address = await addressService.GetAddressByIdAsync(addressId);
                if (address == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(address);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody] AddressDto address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var newAddress = new Address
                {
                    AddressLine = address.AddressLine,
                    PinCode = address.PinCode,
                    City = address.City,
                    Landmark = address.Landmark,
                    Country = address.Country,
                    CountryCode = address.CountryCode,
                    PhoneNumber = address.PhoneNumber,
                };
                await addressService.AddAddressAsync(newAddress);
                var locationUri = Url.Action("GetAddressById", new { addressId = newAddress.AddressId });
                return Created(locationUri, newAddress);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressDto updatedAddress)
        {
            try
            {
                var result = await addressService.UpdateAddressAsync(updatedAddress);
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
        [Route("{addressId:Guid}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid addressId)
        {
            try
            {
                var deleteStatus = await addressService.DeleteAddressAsync(addressId);
                return Ok(deleteStatus);
            }         
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
