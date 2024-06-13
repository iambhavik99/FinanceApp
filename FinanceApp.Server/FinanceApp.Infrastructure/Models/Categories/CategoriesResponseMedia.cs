using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Models.Categories
{
    public class CategoriesResponseMedia
    {
        public List<Category> items { get; set; } = new List<Category>();
    }

    public class Category
    {
        public Guid id { get; set; }
        public string name { get; set; }
    }
}
