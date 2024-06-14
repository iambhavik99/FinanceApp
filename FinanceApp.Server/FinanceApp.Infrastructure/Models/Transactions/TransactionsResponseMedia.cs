using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Models.Transactions
{
    public class TransactionsRequestMedia
    {
        public Guid accountId { get; set; }
        public Guid categoryId { get; set; }
        public decimal amount { get; set; }
        public string note { get; set; }
        public string transactionType { get; set; }
    }
}
