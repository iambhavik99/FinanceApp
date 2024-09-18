using FinanceApp.Domain.DBContext;
using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Common;
using FinanceApp.Infrastructure.Models.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FinanceApp.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDBContext _context;
        public TransactionRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<TransactionsResponseMedia> GetAllTransactions(Guid usersId, PaginationModel paginationModel)
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
            transactionsResponseMedia.totalRecords = response.Count;
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

            if (paginationModel.sortBy == "asc")
            {

                transactionsResponseMedia.items = transactionsResponseMedia.items
                    .OrderBy(x => paginationModel.sortBy)
                    .Skip(paginationModel.pageIndex * paginationModel.pageSize)
                    .Take(paginationModel.pageSize)
                    .ToList();
            }
            else
            {
                transactionsResponseMedia.items = transactionsResponseMedia.items
                    .OrderByDescending(x => paginationModel.sortBy)
                    .Skip(paginationModel.pageIndex * paginationModel.pageSize)
                    .Take(paginationModel.pageSize)
                    .ToList();
            }

            return transactionsResponseMedia;

        }

        public async Task<bool> SaveTransaction(TransactionsRequestMedia transactionsRequestMedia, Guid userId, bool updateBalanceAllowed)
        {
            try
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

                if (transactions.type == TransactionType.CREDIT && updateBalanceAllowed)
                {
                    account.balance += transactions.amount;
                }
                else if (transactions.type == TransactionType.DEBIT && updateBalanceAllowed)
                {
                    account.balance -= transactions.amount;
                }

                account.updatedAt = DateTime.UtcNow;

                AccountHistory accountHistory = new AccountHistory();
                accountHistory.id = Guid.NewGuid();
                accountHistory.accountId = account.id;
                accountHistory.transactionId = transactions.id;
                accountHistory.categoryId = transactions.categoryId;
                accountHistory.userId = transactions.userId;
                accountHistory.amount = transactions.amount;
                accountHistory.balance = account.balance;
                accountHistory.createdAt = transactions.createdAt;

                await _context.Transactions.AddAsync(transactions);
                await _context.AccountHistories.AddAsync(accountHistory);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getDescription(TransactionsRequestMedia transactionsRequestMedia, Guid transactionId)
        {
            string note = transactionsRequestMedia.note.ToUpper();
            if (note == "INITIAL BALANCE")
            {
                return note;
            }

            string accountId = transactionsRequestMedia.accountId.ToString().Substring(0, 8);
            string _transactionId = transactionId.ToString().Substring(0, 8);

            if (note.Length > 12)
            {
                note = note.Substring(0, 12);
            }

            return $"{transactionsRequestMedia.transactionType}/{_transactionId}/{accountId}/{note}";
        }
    }
}
