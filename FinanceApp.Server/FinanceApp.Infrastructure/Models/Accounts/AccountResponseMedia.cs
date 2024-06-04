using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Models.Accounts
{
    public class AccountResponseMedia
    {
        public List<AccountsList> items { get; set; } = new List<AccountsList>();
    }

    public class AccountsList
    {
        public Guid accountId { get; set; }
        public string accountName { get; set; }
        public int balance { get; set; } = 0;

    }
}
