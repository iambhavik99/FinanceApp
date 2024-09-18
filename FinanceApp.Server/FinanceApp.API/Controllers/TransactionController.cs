using FinanceApp.Application.Interfaces;
using FinanceApp.Application.Services;
using FinanceApp.Infrastructure.Models.Accounts;
using FinanceApp.Infrastructure.Models.Common;
using FinanceApp.Infrastructure.Models.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceApp.API.Controllers
{
    [Route("api/transactions")]
    [Authorize]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IAccountHistoryRepository _accountHistoryRepository;
        public TransactionController(ITransactionService transactionService, IAccountHistoryRepository accountHistoryRepository)
        {
            _transactionService = transactionService;
            _accountHistoryRepository = accountHistoryRepository;
        }

        [HttpPost]
        [Route("history")]
        public async Task<ActionResult<TransactionsResponseMedia>> GetAllTransactions(PaginationModel paginationModel)
        {
            Guid userId = new Guid(User?.Claims?.FirstOrDefault(c => c?.Type == ClaimTypes.Actor).Value?.ToString());
            var response = await _transactionService.GetAllTransactions(userId, paginationModel);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> SaveTransaction(TransactionsRequestMedia transactionsRequestMedia)
        {
            Guid userId = new Guid(User?.Claims?.FirstOrDefault(c => c?.Type == ClaimTypes.Actor).Value?.ToString());
            var response = await _transactionService.SaveTransaction(transactionsRequestMedia, userId);
            if (response == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("history")]
        public async Task<ActionResult> GetTransactionHistory()
        {
            Guid userId = new Guid(User?.Claims?.FirstOrDefault(c => c?.Type == ClaimTypes.Actor).Value?.ToString());
            var response = await _accountHistoryRepository.GetTransactionHistory(userId);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
