using FinanceApp.Application.Interfaces;
using FinanceApp.Domain.DBContext;
using FinanceApp.Infrastructure.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Services
{
    public class AccountHistoryRepository : IAccountHistoryRepository
    {
        private readonly ApplicationDBContext _context;
        public AccountHistoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<AccountTransactionHistoryResponseMedia> GetTransactionHistory(Guid userId)
        {
            try
            {
                var response = await (
                    from transaction in _context.Transactions
                    join history in _context.AccountHistories
                    on transaction.id equals history.transactionId
                    join category in _context.Categories
                    on transaction.categoryId equals category.id
                    where transaction.userId == userId
                    select new
                    {
                        transactionId = transaction.id,
                        accountId = transaction.accountId,
                        categoryName= category.name,
                        amount = history.amount,
                        balance = history.balance,
                        description = transaction.description,
                        type = transaction.type,
                        timestamp = history.createdAt
                    })
                    .ToListAsync();

                if (response == null || response.Count == 0)
                {
                    return new AccountTransactionHistoryResponseMedia();
                }

                var accountTransactionHistoryResponseMedia = new AccountTransactionHistoryResponseMedia();
                foreach (var item in response)
                {
                    DateTimeOffset dto = new DateTimeOffset(item.timestamp);
                    var unixTime = dto.ToUnixTimeMilliseconds();

                    accountTransactionHistoryResponseMedia.items
                        .Add(new AccountTransactionMedia()
                        {
                            accountId = item.accountId,
                            amount = item.amount,
                            balance = item.balance,
                            categoryName = item.categoryName,
                            description = item.description,
                            transactionId = item.transactionId,
                            type = item.type,
                            timestamp = unixTime
                        });
                }

                var items = accountTransactionHistoryResponseMedia.items
                    .OrderByDescending(a => a.timestamp)
                    .Take(5).ToList();

                // Get the current time and subtract 24 hours in milliseconds
                var currentTimeMillis = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                var last24HoursMillis = currentTimeMillis - 24 * 60 * 60 * 1000;

                var records = items.Where(x => x.timestamp >= last24HoursMillis)
                    .Select(item => new AccountTransactionPreviewMedia()
                    {
                        balance = item.balance,
                        timestamp = item.timestamp
                    })
                    .Take(10)
                    .ToList();

                accountTransactionHistoryResponseMedia.items = items;
                accountTransactionHistoryResponseMedia.records = records;

                return accountTransactionHistoryResponseMedia;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
