using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Domain.Models
{
    public class Users
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        [MaxLength(100)]
        public string username { get; set; }
        [Required]
        [MaxLength(100)]
        public string email { get; set; }
        [MaxLength(100)]
        public string password { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public ICollection<Accounts> Accounts { get; set; }
        public ICollection<Transactions> Transactions { get; set; }

    }
}
