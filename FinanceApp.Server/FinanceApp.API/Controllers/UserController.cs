using FinanceApp.Application.Interfaces;
using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Models.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinanceApp.API
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserRequestMedia userRequestMedia)
        {
            var response = await _userService.SignUp(userRequestMedia);
            if (response != null)
            {
                Console.WriteLine(HttpContext.Request.Headers.Cookie.ToString());
                return Ok(response);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequestMedia userLoginRequestMedia)
        {

            string aesKeyString = _configuration.GetSection("AES:Key").Value;
            string ivString = _configuration.GetSection("AES:IV").Value;

            var response = await _userService.login(userLoginRequestMedia, aesKeyString, ivString);
            if (response != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, response.email),
                    new Claim(ClaimTypes.Name, response.username),
                    new Claim(ClaimTypes.Actor, response.id.ToString()),
                    new Claim(ClaimTypes.Expiration,DateTime.UtcNow.AddDays(1).ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = true,
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    });

                return Ok(response);
            }
            return BadRequest();
        }


        [Authorize]
        [HttpGet("info")]
        public async Task<IActionResult> GetUserInfo()
        {
            Guid userId = new Guid(User?.Claims?.FirstOrDefault(c => c?.Type == ClaimTypes.Actor).Value?.ToString());
            var response = await _userService.GetUserInfo(userId);

            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();

        }
    }
}
