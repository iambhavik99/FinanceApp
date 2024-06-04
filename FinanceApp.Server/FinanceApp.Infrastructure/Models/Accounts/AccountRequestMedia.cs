using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Models.Accounts
{
    public class AccountRequestMedia
    {
        public string accountName { get; set; }
        public int balance { get; set; } = 0;
        public Guid userId { get; set; }
    }
}
