using FinanceApp.Application.Interfaces;
using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<TransactionsResponseMedia> GetAllTransactions(Guid accountId)
        {
            var response = await _transactionRepository.GetAllTransactions(accountId);
            return response;
        }

        public async Task<TransactionsResponseMedia> SaveTransaction(TransactionsRequestMedia transactionsRequestMedia)
        {
            var response = await _transactionRepository.SaveTransaction(transactionsRequestMedia);
            return response;
        }
    }
}
