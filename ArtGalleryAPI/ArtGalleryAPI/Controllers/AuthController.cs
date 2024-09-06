using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ITokenInterface tokenService;

        public AuthController(UserManager<AppUser> userManager, ITokenInterface tokenService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        /// <summary>
        /// login for users
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var identityUser = await userManager.FindByEmailAsync(request.Email);
            if (identityUser is not null)
            {
                // Check Password
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);

                    // Create a Token and Response
                    var jwtToken = tokenService.CreateJwtToken(identityUser, roles.ToList());

                    var response = new LoginResponseDto()
                    {
                        Id = identityUser.Id,
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = jwtToken
                    };

                    return Ok(response);
                }
            }
            ModelState.AddModelError("", "Email or Password Incorrect");


            return ValidationProblem(ModelState);
        }

        /// <summary>
        /// register for normal users
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var user = new AppUser()
            {
                UserName = request.Email.Trim(),
                Email = request.Email.Trim(),
                FirstName = request.FirstName.Trim(),
                LastName = request?.LastName?.Trim(),
                CountryCode = request?.CountryCode?.Trim(),
                PhoneNumber = request?.PhoneNumber?.Trim(),
                Status = "Active",
                CreatedAt = DateTime.UtcNow,
            };
            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRoleAsync(user, "Reader");
                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }

        /// <summary>
        /// Add user for admin user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("admin/register")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> RegisterForAdmin([FromBody] RegisterRequestDto request)
        {
            var user = new AppUser()
            {
                UserName = request.Email.Trim(),
                Email = request.Email.Trim(),
                FirstName = request.FirstName.Trim(),
                LastName = request?.LastName?.Trim(),
                CountryCode = request?.CountryCode?.Trim(),
                PhoneNumber = request?.PhoneNumber?.Trim(),
                Status = "Active",
                CreatedAt = DateTime.UtcNow,
            };
            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (identityResult.Succeeded)
            {
                if (request.isAdmin)
                {
                    identityResult = await userManager.AddToRoleAsync(user, "Reader");
                    identityResult = await userManager.AddToRoleAsync(user, "Writer");
                }
                else
                {
                    identityResult = await userManager.AddToRoleAsync(user, "Reader");
                }
                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (identityResult.Errors.Any())
                    {
                        foreach (var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return ValidationProblem(ModelState);
        }

        /// <summary>
        /// Add admin role to an user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("admin/addAdmin/{userId}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddAdminRole([FromRoute] string userId)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    var identityUser = await userManager.FindByIdAsync(userId);
                    if (identityUser is not null)
                    {
                        IdentityResult identityResult = await userManager.AddToRoleAsync(identityUser, "Writer");
                        if (identityResult.Succeeded)
                        {
                            return Ok();
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove admin role to an user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("admin/removeAdmin/{userId}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> RemoveAdminRole([FromRoute] string userId)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    var identityUser = await userManager.FindByIdAsync(userId);
                    if (identityUser is not null)
                    {
                        IdentityResult identityResult = await userManager.RemoveFromRoleAsync(identityUser, "Writer");
                        if (identityResult.Succeeded)
                        {
                            return Ok();
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
