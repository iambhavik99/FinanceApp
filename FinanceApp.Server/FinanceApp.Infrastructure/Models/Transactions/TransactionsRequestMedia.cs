using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Models.Transactions
{
    public class TransactionsResponseMedia
    {
       public List<TransactionMedia> items { get; set; }
    }

    public class TransactionMedia
    {
        public Guid transactionId { get; set; }
        public Guid accountId { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public string transactionType { get; set; }

    }

    
}
