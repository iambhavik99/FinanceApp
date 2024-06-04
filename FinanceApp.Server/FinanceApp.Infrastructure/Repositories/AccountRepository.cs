using FinanceApp.Domain.DBContext;
using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDBContext _context;
        public AccountRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<AccountResponseMedia> GetAccounts(Guid userId)
        {
            var response = await _context.Accounts
                .Where(account => account.userId == userId)
                .ToListAsync();

            var accountResponseMedia = new AccountResponseMedia();
            if (response.Count == 0)
            {
                return accountResponseMedia;
            }

            accountResponseMedia.items = response.ToArray().Select(x => new AccountsList()
            {
                accountId = x.id,
                accountName = x.accountName,
                balance = x.balance,
            }).ToList();

            return accountResponseMedia;

        }

        public async Task<AccountResponseMedia> SaveAccount(AccountRequestMedia accountRequestMedia)
        {
            Accounts account = new Accounts();
            account.id = Guid.NewGuid();
            account.accountName = accountRequestMedia.accountName;
            account.balance = accountRequestMedia.balance;
            account.userId = accountRequestMedia.userId;
            account.createdAt = DateTime.UtcNow;
            account.updatedAt = DateTime.UtcNow;

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return await GetAccounts(accountRequestMedia.userId);

        }
    }
}
