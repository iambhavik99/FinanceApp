using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Models
{
    public class Accounts
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        [ForeignKey("userId")]
        public Guid userId { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public decimal balance { get; set; } = 0;
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        
        public ICollection<Transactions> Transactions { get; set; }
        public ICollection<AccountHistory> AccountHistory { get; set; }


        public Users User { get; set; }
    }
}
