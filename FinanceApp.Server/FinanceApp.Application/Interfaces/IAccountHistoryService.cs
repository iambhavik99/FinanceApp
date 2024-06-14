using FinanceApp.Infrastructure.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Interfaces
{
    public interface IAccountHistoryService
    {
        public Task<AccountTransactionHistoryResponseMedia> GetTransactionHistory(Guid userId);
    }
}
