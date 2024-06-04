using FinanceApp.Application.Interfaces;
using FinanceApp.Application.Services;
using FinanceApp.Infrastructure.Models.Accounts;
using FinanceApp.Infrastructure.Models.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.API.Controllers
{
    [ApiController]
    [Route("api/transactions")]
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
            var response = await _transactionService.SaveTransaction(transactionsRequestMedia);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
