﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Models
{
    public class Transactions
    {
        [Key]
        public Guid id { get; set; }
        [ForeignKey("accountId")]
        public Guid accountId { get; set; }
        [ForeignKey("userId")]
        public Guid userId { get; set; }
        [Required]
        public decimal amount { get; set; }
        [Required]
        public string type { get; set; }
        [ForeignKey("categoryId")]
        public Guid categoryId { get; set; }
        public string description { get; set; }
        public string note { get; set; }
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        public ICollection<AccountHistory> AccountHistory { get; set; }


        public Users User { get; set; }
        public Accounts Account { get; set; }
        public Categories Category { get; set; }

    }
}
