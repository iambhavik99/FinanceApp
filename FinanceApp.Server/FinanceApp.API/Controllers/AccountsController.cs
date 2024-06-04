using FinanceApp.Application.Interfaces;
using FinanceApp.Infrastructure.Models.Accounts;
using FinanceApp.Infrastructure.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.API.Controllers
{

    [ApiController]
    [Route("api/accounts")]
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
    }
}
