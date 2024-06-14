using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Models.Accounts
{
    public class AccountTransactionHistoryResponseMedia
    {
        public List<AccountTransactionMedia> items { get; set; } = new List<AccountTransactionMedia>();
    }

    public class AccountTransactionMedia
    {
        public Guid accountId { get; set; }
        public Guid transactionId { get; set; }
        public Guid categoryId { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
        public decimal balance { get; set; }
        public int timestamp { get; set; }
    }

}
