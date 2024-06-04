using FinanceApp.Domain.DBContext;
using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FinanceApp.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDBContext _context;
        public TransactionRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<TransactionsResponseMedia> GetAllTransactions(Guid accountId)
        {
            var response = await _context.Transactions
                .Where(transaction => transaction.accountId == accountId)
                .ToListAsync();

            TransactionsResponseMedia transactionsResponseMedia = new TransactionsResponseMedia();
            transactionsResponseMedia.items = response
                .Select(x => new TransactionMedia
                {
                    accountId = accountId,
                    amount = x.amount,
                    description = x.description,
                    transactionId = x.id,
                    transactionType = x.transactionType
                })
                .ToList();

            return transactionsResponseMedia;

        }

        public async Task<TransactionsResponseMedia> SaveTransaction(TransactionsRequestMedia transactionsRequestMedia)
        {
            Transactions transactions = new Transactions();
            transactions.id = Guid.NewGuid();
            transactions.amount = transactionsRequestMedia.amount;
            transactions.description = transactionsRequestMedia.description;
            transactions.accountId = transactionsRequestMedia.accountId;
            transactions.transactionType = transactionsRequestMedia.transactionType;
            transactions.transactionDate = DateTime.UtcNow;
            transactions.createdAt = DateTime.UtcNow;

            // Update the account balance based on transaction type
            var account = await _context.Accounts.FindAsync(transactions.accountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            if (transactions.transactionType == TransactionType.CREDIT)
            {
                account.balance += transactions.amount;
            }
            else if (transactions.transactionType == TransactionType.DEBIT)
            {
                account.balance -= transactions.amount;
            }

            await _context.Transactions.AddAsync(transactions);
            await _context.SaveChangesAsync();

            return await GetAllTransactions(transactions.accountId);
        }
    }
}
