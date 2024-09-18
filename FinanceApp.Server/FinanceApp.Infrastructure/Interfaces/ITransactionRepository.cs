using FinanceApp.Infrastructure.Models.Common;
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
        public Task<TransactionsResponseMedia> GetAllTransactions(Guid accountId, PaginationModel paginationModel);
        public Task<bool> SaveTransaction(TransactionsRequestMedia transactionsRequestMedia, Guid userId, bool updateBalanceAllowed = true);

    }
}
