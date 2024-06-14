using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Models
{
    public class AccountHistory
    {
        [Key]
        public Guid id { get; set; }
        [ForeignKey("accountId")]
        public Guid accountId { get; set; }
        [ForeignKey("transactionId")]
        public Guid transactionId { get; set; }
        [ForeignKey("userId")]
        public Guid userId { get; set; }
        [ForeignKey("categoryId")]
        public Guid categoryId { get; set; }
        public decimal amount { get; set; } = 0;
        public decimal balance { get; set; } = 0;
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        
        
        public Users Users { get; set; }
        public Accounts Account { get; set; }
        public Categories Category { get; set; }
        public Transactions Transaction { get; set; }

    }
}
