using FinanceApp.Application.Interfaces;
using FinanceApp.Application.Services;
using FinanceApp.Infrastructure.Models.Accounts;
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
        public TransactionController(ITransactionService transactionService) { 
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<ActionResult<TransactionsResponseMedia>> GetAllTransactions(Guid userId)
        {
            var response = await _transactionService.GetAllTransactions(userId);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<TransactionsResponseMedia>> SaveTransaction(TransactionsRequestMedia transactionsRequestMedia)
        {
            Guid userId = new Guid(User?.Claims?.FirstOrDefault(c => c?.Type == ClaimTypes.Actor).Value?.ToString());
            var response = await _transactionService.SaveTransaction(transactionsRequestMedia, userId);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
