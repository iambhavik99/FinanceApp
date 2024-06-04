using FinanceApp.Application.Interfaces;
using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.API
{
    [ApiController]
    [Route("api/users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> AddUser(UserRequestMedia userRequestMedia)
        {
            var response = await _userService.AddUser(userRequestMedia);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

    }
}
