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
                    where transaction.userId == userId
                    select new
                    {
                        transactionId = transaction.id,
                        accountId = transaction.accountId,
                        categoryId = history.categoryId,
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
                    accountTransactionHistoryResponseMedia.items
                        .Add(new AccountTransactionMedia()
                        {
                            accountId = item.accountId,
                            amount = item.amount,
                            balance = item.balance,
                            categoryId = item.categoryId,
                            description = item.description,
                            transactionId = item.transactionId,
                            type = item.type,
                            timestamp = item.timestamp.Millisecond
                        });
                }

                accountTransactionHistoryResponseMedia.items = accountTransactionHistoryResponseMedia.items
                    .OrderBy(a => a.timestamp)
                    .ToList();

                return accountTransactionHistoryResponseMedia;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
