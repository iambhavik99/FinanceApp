using System;
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
        public Guid accountId { get; set; }
        [Required]
        public int amount { get; set; }
        [Required]
        public string description { get; set; }
        public string transactionType { get; set; }
        public DateTime transactionDate { get; set; } = DateTime.UtcNow;
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public Accounts Account { get; set; }

    }
}
