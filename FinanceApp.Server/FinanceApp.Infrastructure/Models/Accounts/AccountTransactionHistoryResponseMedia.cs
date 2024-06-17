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
        public List<AccountTransactionPreviewMedia> records { get; set; } = new List<AccountTransactionPreviewMedia>();
    }

    public class AccountTransactionMedia
    {
        public Guid accountId { get; set; }
        public Guid transactionId { get; set; }
        public string categoryName { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
        public decimal balance { get; set; }
        public long timestamp { get; set; }
    }

    public class AccountTransactionPreviewMedia
    {
        public long timestamp { get; set; }
        public decimal balance { get; set; }
    }

}
