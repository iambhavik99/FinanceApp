using FinanceApp.Infrastructure.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Interfaces
{
    public interface ITransactionService
    {
        public Task<TransactionsResponseMedia> GetAllTransactions(Guid accountId, int limit);
        public Task<bool> SaveTransaction(TransactionsRequestMedia transactionsRequestMedia, Guid userId);
    }
}
