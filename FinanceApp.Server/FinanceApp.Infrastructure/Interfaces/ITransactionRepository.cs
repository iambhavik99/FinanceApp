using FinanceApp.Infrastructure.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<TransactionsResponseMedia> GetAllTransactions(Guid accountId, int limit);
        public Task<bool> SaveTransaction(TransactionsRequestMedia transactionsRequestMedia, Guid userId);

    }
}
