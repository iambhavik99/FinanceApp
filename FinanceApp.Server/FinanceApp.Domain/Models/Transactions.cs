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
        [ForeignKey("accountId")]
        public Accounts Account { get; set; }
        public Guid userId { get; set; }
        [ForeignKey("userId")]
        public Users User { get; set; }
        [Required]
        public int amount { get; set; }
        [Required]
        public string type { get; set; }
        public Guid categoryId { get; set; }
        [ForeignKey("categoryId")]
        public Categories Category { get; set; }
        public string description { get; set; }
        public string note { get; set; }
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

    }
}
