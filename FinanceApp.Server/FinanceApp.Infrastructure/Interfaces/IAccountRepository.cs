using FinanceApp.Infrastructure.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Interfaces
{
    public interface IAccountRepository
    {
        public Task<AccountResponseMedia> GetAccounts(Guid userId);
        public Task<AccountResponseMedia> SaveAccount(AccountRequestMedia accountRequestMedia);
    }
}
