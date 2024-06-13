using FinanceApp.Application.Interfaces;
using FinanceApp.Infrastructure.Models.Accounts;
using FinanceApp.Infrastructure.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceApp.API.Controllers
{

    [Route("api/accounts")]
    [Authorize]
    [ApiController]
    public class AccountsController : Controller
    {

        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<ActionResult<AccountResponseMedia>> GetAccounts(Guid userId)
        {
            var response = await _accountService.GetAccounts(userId);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<AccountResponseMedia>> SaveAccount(AccountRequestMedia accountRequestMedia)
        {
            var response = await _accountService.SaveAccount(accountRequestMedia);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpGet("metadata")]
        public async Task<ActionResult<AccountResponseMedia>> GetAccountMetadata()
        {
            Guid userId = new Guid(User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.Actor).Value.ToString());
            var response = await _accountService.GetAccountMetadata(userId);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
