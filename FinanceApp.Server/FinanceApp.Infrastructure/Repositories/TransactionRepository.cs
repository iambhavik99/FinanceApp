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
        public async Task<TransactionsResponseMedia> GetAllTransactions(Guid usersId)
        {
            var response = await (
                from accout in _context.Accounts
                join transaction in _context.Transactions
                on accout.id equals transaction.accountId
                where accout.userId == usersId
                select transaction
                )
                .ToListAsync();

            TransactionsResponseMedia transactionsResponseMedia = new TransactionsResponseMedia();
            transactionsResponseMedia.items = response
                .Select(x => new TransactionMedia
                {
                    transactionId = x.id,
                    accountId = x.accountId,
                    categoryId = x.categoryId,
                    amount = x.amount,
                    description = x.description,
                    transactionType = x.type,
                    note = x.note,
                    transactionDate = x.createdAt
                })
                .ToList();

            return transactionsResponseMedia;

        }

        public async Task<TransactionsResponseMedia> SaveTransaction(TransactionsRequestMedia transactionsRequestMedia, Guid userId)
        {
            Transactions transactions = new Transactions();
            transactions.id = Guid.NewGuid();
            transactions.accountId = transactionsRequestMedia.accountId;
            transactions.userId = userId;
            transactions.categoryId = transactionsRequestMedia.categoryId;
            transactions.amount = transactionsRequestMedia.amount;
            transactions.note = transactionsRequestMedia.note;
            transactions.type = transactionsRequestMedia.transactionType;
            transactions.description = getDescription(transactionsRequestMedia, transactions.id);
            transactions.createdAt = DateTime.UtcNow;

            // Update the account balance based on transaction type
            var account = await _context.Accounts.FindAsync(transactions.accountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }

            if (transactions.type == TransactionType.CREDIT)
            {
                account.balance += transactions.amount;
            }
            else if (transactions.type == TransactionType.DEBIT)
            {
                account.balance -= transactions.amount;
            }

            account.updatedAt = DateTime.UtcNow;

            await _context.Transactions.AddAsync(transactions);
            await _context.SaveChangesAsync();

            return await GetAllTransactions(userId);
        }


        public string getDescription(TransactionsRequestMedia transactionsRequestMedia, Guid transactionId)
        {
            string accountId = transactionsRequestMedia.accountId.ToString().Substring(0,4);
            string _transactionId = transactionId.ToString().Substring(0, 4);
            string note = transactionsRequestMedia.note.ToUpper();

            if (note.Length > 4)
            {
                note = note.Substring(0, 4);
            }

            return $"{_transactionId}-{accountId}-{note}";
        }
    }
}
