using FinanceApp.Domain.DBContext;
using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Accounts;
using FinanceApp.Infrastructure.Models.Categories;
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
        private readonly ITransactionRepository _transactionRepository;
        public AccountRepository(ApplicationDBContext context, ITransactionRepository transactionRepository)
        {
            _context = context;
            _transactionRepository = transactionRepository;
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

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.name == "INITIAL_BALANCE");

            TransactionsRequestMedia transactionsRequestMedia = new TransactionsRequestMedia();
            transactionsRequestMedia.accountId = account.id;
            transactionsRequestMedia.amount = accountRequestMedia.balance;
            transactionsRequestMedia.note = "INITIAL BALANCE";
            transactionsRequestMedia.categoryId = category.id;
            transactionsRequestMedia.transactionType = TransactionType.CREDIT;

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            await _transactionRepository.SaveTransaction(transactionsRequestMedia, accountRequestMedia.userId, false);

            return await GetAccounts(accountRequestMedia.userId);

        }

        public async Task<AccountMetadataMedia> GetAccountMetadata(Guid userId)
        {
            try
            {
                var accounts = await _context.Accounts
                    .Where(a => a.userId == userId)
                    .Select(a => new
                    {
                        name = a.name,
                        totalBalance = a.balance
                    })
                    .ToListAsync();

                var transactions = await _context.Transactions
                    .Where(t => t.userId == userId)
                    .ToListAsync();

                var totalBalance = accounts.Sum(x => x.totalBalance);
                var totalExpense = transactions
                    .Where(t => t.type == TransactionEum.DEBIT)
                    .Sum(t => t.amount);
                var totalIncome = transactions
                    .Where(t => t.type == TransactionEum.CREDIT)
                    .Sum(t => t.amount);

                var expanses = await (
                    from transaction in _context.Transactions
                    where transaction.userId == userId && transaction.type == TransactionType.DEBIT
                    join category in _context.Categories
                    on transaction.categoryId equals category.id
                    select new
                    {
                        category = category.name,
                        amount = transaction.amount
                    })
                    .GroupBy(grp => grp.category)
                    .Select(g => new AccountExpanses
                    {
                        categoryName = g.Key,
                        amount = g.Sum(x => x.amount)
                    })
                    .ToListAsync();



                var transactionByMonthsMedias = new Dictionary<string, TransactionByMonthsMedia>();

                foreach (var transaction in transactions.OrderBy(x => x.createdAt))
                {
                    var monthName = transaction.createdAt.ToString("MMM yy");

                    if (!transactionByMonthsMedias.TryGetValue(monthName, out var transactionByMonthMedia))
                    {
                        transactionByMonthMedia = new TransactionByMonthsMedia() { month = monthName };
                        transactionByMonthsMedias[monthName] = transactionByMonthMedia;
                    }

                    if (transaction.type == TransactionType.DEBIT)
                    {
                        transactionByMonthMedia.expanse += transaction.amount;
                    }
                    else
                    {
                        transactionByMonthMedia.income += transaction.amount;
                    }
                }

                return new AccountMetadataMedia
                {
                    totalBalance = totalBalance,
                    totalExpance = totalExpense,
                    totalIncome = totalIncome,
                    expanses = expanses,
                    transactions = transactionByMonthsMedias.Values.ToList(),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
