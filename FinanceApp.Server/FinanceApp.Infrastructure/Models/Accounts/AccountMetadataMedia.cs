using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Models.Accounts
{
    public class AccountMetadataMedia
    {
        public decimal totalBalance { get; set; } = 0;
        public decimal totalExpance { get; set; } = 0;
        public decimal totalIncome { get; set; } = 0;
        public List<AccountExpanses> expanses { get; set; } = new List<AccountExpanses>();
        public List<TransactionByMonthsMedia> transactions { get; set; } = new List<TransactionByMonthsMedia> ();
    }

    public class AccountExpanses
    {
        public string categoryName { get; set; }
        public decimal amount { get; set; } = 0;
    }

    public class TransactionByMonthsMedia
    {
        public decimal income { get; set; } = 0;
        public decimal expanse { get; set; } = 0;
        public string month { get; set; }
    }

}
