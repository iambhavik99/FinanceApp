using FinanceApp.Application.Interfaces;
using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Repositories
{
    public class AccountHistoryService : IAccountHistoryService
    {
        private readonly IAccountHistoryRepository _accountHistoryRepository;
        public AccountHistoryService(IAccountHistoryRepository accountHistoryRepository)
        {
            _accountHistoryRepository = accountHistoryRepository;
        }

        public async Task<AccountTransactionHistoryResponseMedia> GetTransactionHistory(Guid userId)
        {
            return await _accountHistoryRepository.GetTransactionHistory(userId);
        }

    }
}
