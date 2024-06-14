﻿using System;
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
        public List<AccountMetadata> accounts { get; set; } = new List<AccountMetadata>();
    }

    public class AccountMetadata
    {
        public string name { get; set; }
        public decimal totalBalance { get; set; } = 0;
    }
}
