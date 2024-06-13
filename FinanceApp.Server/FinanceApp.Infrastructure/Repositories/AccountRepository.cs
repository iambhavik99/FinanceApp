using FinanceApp.Domain.DBContext;
using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Accounts;
using FinanceApp.Infrastructure.Models.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                accountName = x.name,
                balance = x.balance,
            }).ToList();

            return accountResponseMedia;

        }

        public async Task<AccountResponseMedia> SaveAccount(AccountRequestMedia accountRequestMedia)
        {
            Accounts account = new Accounts();
            account.id = Guid.NewGuid();
            account.name = accountRequestMedia.accountName;
            account.balance = accountRequestMedia.balance;
            account.userId = accountRequestMedia.userId;
            account.createdAt = DateTime.UtcNow;
            account.updatedAt = DateTime.UtcNow;

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return await GetAccounts(accountRequestMedia.userId);

        }

        public async Task<AccountMetadataMedia> GetAccountMetadata(Guid userId)
        {
            try
            {
                var accounts = await _context.Accounts.Where(a => a.userId == userId).ToListAsync();
                var transactions = await _context.Transactions.Where(t => t.userId == userId).ToListAsync();

                List<AccountMetadata> accountMetadata = new List<AccountMetadata>();
                accountMetadata = accounts.Select(x => new AccountMetadata { name = x.name, totalBalance = x.balance })
                    .ToList();

                return new AccountMetadataMedia
                {
                    accounts = accountMetadata,
                    totalBalance = accounts.Sum(x => x.balance),
                    totalExpance = transactions.Where(t => t.type == TransactionEum.DEBIT).Sum(t => t.amount),
                    totalIncome = transactions.Where(t => t.type == TransactionEum.CREDIT).Sum(t => t.amount)
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
