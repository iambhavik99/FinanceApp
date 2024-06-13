using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Models.Accounts
{
    public class AccountMetadataMedia
    {
        public double totalBalance { get; set; } = 0.0;
        public double totalExpance { get; set; } = 0.0;
        public double totalIncome { get; set; } = 0.0;
        public List<AccountMetadata> accounts { get; set; } = new List<AccountMetadata>();
    }

    public class AccountMetadata
    {
        public string name { get; set; }
        public double totalBalance { get; set; } = 0.0;
    }
}
