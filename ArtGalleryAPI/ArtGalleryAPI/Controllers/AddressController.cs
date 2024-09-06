using ArtGalleryAPI.CustomExceptions;
using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Implementation;
using ArtGalleryAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressInterface addressService;
        private readonly IAppUserInterface appUserService;

        public AddressController(IAddressInterface addressService, IAppUserInterface appUserService)
        {
            this.addressService = addressService;
            this.appUserService = appUserService;
        }
        [HttpGet]
        [Authorize(Roles = "Writer")]
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
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressDto address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided!");
            }

            try
            {
                var user = await appUserService.GetUserByEmailAsync(address.userEmail);
                if (user != null)
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
                        AppUserId = user.Id,
                    };
                    await addressService.AddAddressAsync(newAddress);
                    var locationUri = Url.Action("GetAddressById", new { addressId = newAddress.AddressId });
                    return Created(locationUri, newAddress);
                }
                else
                {
                    return BadRequest("Invalid data!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("{addressId:Guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateAddress([FromRoute] Guid addressId, [FromBody] UpdateAddressDto updatedAddress)
        {
            try
            {
                var result = await addressService.UpdateAddressAsync(addressId, updatedAddress);
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
        [Authorize]
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
        [HttpGet]
        [Route("AppUser/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetAddressesByUserId([FromRoute] string userId)
        {
            try
            {
                var addresses = await addressService.GetAddressesByUserIdAsync(userId);
                if (addresses == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(addresses);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
