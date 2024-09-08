using ArtGalleryAPI.Models.Domain;
using ArtGalleryAPI.Models.Dto;
using ArtGalleryAPI.Services.Interface;
using Azure.Core;
using ExcelDataReader;
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
        /// Add bulk user for admin user
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("admin/register/bulk")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> RegisterBulkForAdmin([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file!");
            }

            const long maxFileSize = 4 * 1024 * 1024;
            if (file.Length > maxFileSize)
            {
                return BadRequest("File size exceeds limit!");
            }

            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (fileExtension != ".xlsx" && fileExtension != ".xls")
            {
                return BadRequest("Invalid file!");
            }

            List<AppUser> users = new List<AppUser>();
            List<string> expectedHeaders = new List<string>() { "email", "firstname", "lastname", "countrycode", "phonenumber", "isadmin" };
            List<string> actualHeaders = new List<string>();
            bool areHeadersRead = false;
            List<AddBulkUsersResponseDto> addBulkUsersResponse = new List<AddBulkUsersResponseDto>();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (
                    var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
                    {
                        LeaveOpen = false,
                        AutodetectSeparators = new char[] { ',', ';', '\t', '#', '|' },
                    })
                    )
                {
                    while (reader.Read())
                    {
                        var res = new AddBulkUsersResponseDto();
                        if ((String.IsNullOrWhiteSpace(reader.GetString(0)) || reader.RowCount < 2) && areHeadersRead == false)
                        {
                            return BadRequest("Invalid file!");
                        }
                        if (areHeadersRead == false)
                        {
                            if (reader.FieldCount == expectedHeaders.Count)
                            {
                                actualHeaders.Add(reader.GetString(0).ToLower());
                                actualHeaders.Add(reader.GetString(1).ToLower());
                                actualHeaders.Add(reader.GetString(2).ToLower());
                                actualHeaders.Add(reader.GetString(3).ToLower());
                                actualHeaders.Add(reader.GetString(4).ToLower());
                                actualHeaders.Add(reader.GetString(5).ToLower());
                                areHeadersRead = true;
                            }
                            if (!actualHeaders.SequenceEqual(expectedHeaders))
                            {
                                return BadRequest("Invalid file!");
                            }
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("email"))) &&
                           string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("firstname"))) &&
                           string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("lastname"))) &&
                           string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("countrycode"))) &&
                           string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("phonenumber"))) &&
                           string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("isadmin"))))
                        {
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("email"))) ||
                            string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("firstname"))) ||
                            string.IsNullOrWhiteSpace(reader.GetString(actualHeaders.IndexOf("isadmin"))))
                        {
                            res.status = "failed";
                            res.ErrorInfo = "mandatory fields are not filled";
                            addBulkUsersResponse.Add(res);
                            continue;
                        }

                        res.Email = reader.GetString(actualHeaders.IndexOf("email"));
                        res.FirstName = reader.GetString(actualHeaders.IndexOf("firstname"));
                        res.LastName = reader.GetString(actualHeaders.IndexOf("lastname"));
                        res.IsAdmin = reader.GetString(actualHeaders.IndexOf("isadmin"));

                        string regex = "^[a-zA-Z0-9_\\.-]+@([A-Za-z0-9-]+\\.)+[A-Za-z]{2,6}$";
                        if (!System.Text.RegularExpressions.Regex.IsMatch(reader.GetString(actualHeaders.IndexOf("email")), regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                        {
                            res.status = "failed";
                            res.ErrorInfo = "invalid email";
                            addBulkUsersResponse.Add(res);
                            continue;
                        }

                        var user = new AppUser()
                        {
                            UserName = reader.GetString(actualHeaders.IndexOf("email")),
                            Email = reader.GetString(actualHeaders.IndexOf("email")),
                            FirstName = reader.GetString(actualHeaders.IndexOf("firstname")),
                            LastName = reader.GetString(actualHeaders.IndexOf("lastname")),
                            CountryCode = reader.GetString(actualHeaders.IndexOf("countrycode")),
                            PhoneNumber = reader.GetString(actualHeaders.IndexOf("phonenumber")),
                            Status = "Active",
                            CreatedAt = DateTime.UtcNow,
                        };
                        var isAdmin = reader.GetString(actualHeaders.IndexOf("isadmin")).ToLower();

                        var identityResult = await userManager.CreateAsync(user, "User@123");

                        if (identityResult.Succeeded)
                        {
                            if (isAdmin == "yes")
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
                                res.status = "success";
                            }
                            else
                            {
                                //if (identityResult.Errors.Any())
                                //{
                                //    foreach (var error in identityResult.Errors)
                                //    {
                                //        ModelState.AddModelError("", error.Description);
                                //    }
                                //}
                                res.status = "failed";
                                res.ErrorInfo = "invalid info!";
                            }
                        }
                        else
                        {
                            //if (identityResult.Errors.Any())
                            //{
                            //    foreach (var error in identityResult.Errors)
                            //    {
                            //        ModelState.AddModelError("", error.Description);
                            //    }
                            //}
                            res.status = "failed";
                            res.ErrorInfo = "invalid info!";
                        }
                        addBulkUsersResponse.Add(res);
                    }
                }
            }
            return Ok(addBulkUsersResponse);
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
