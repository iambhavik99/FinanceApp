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
        public Guid userId { get; set; }
        [ForeignKey("userId")]
        public Users User { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int balance { get; set; } = 0;
        public DateTime createdAt { get; set; } = DateTime.UtcNow;
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Transactions> Transactions { get; set; }
    }
}
