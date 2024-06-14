using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Models
{
    public class Categories
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }
        public ICollection<Transactions> Transaction { get; set; }
        public ICollection<AccountHistory> AccountHistory { get; set; }
    }
}
