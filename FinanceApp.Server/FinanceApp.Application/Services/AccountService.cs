using FinanceApp.Application.Interfaces;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task<AccountResponseMedia> GetAccounts(Guid userId)
        {
            return _accountRepository.GetAccounts(userId);
        }

        public Task<AccountResponseMedia> SaveAccount(AccountRequestMedia accountRequestMedia)
        {
            return _accountRepository.SaveAccount(accountRequestMedia);
        }
    }

}
