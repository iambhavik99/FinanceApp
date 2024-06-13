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

        public async Task<AccountResponseMedia> GetAccounts(Guid userId)
        {
            return await _accountRepository.GetAccounts(userId);
        }

        public async Task<AccountResponseMedia> SaveAccount(AccountRequestMedia accountRequestMedia)
        {
            return await _accountRepository.SaveAccount(accountRequestMedia);
        }

        public async Task<AccountMetadataMedia> GetAccountMetadata(Guid userId)
        {
            return await _accountRepository.GetAccountMetadata(userId);
        }
    }

}
