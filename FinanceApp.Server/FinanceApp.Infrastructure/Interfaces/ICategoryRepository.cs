using FinanceApp.Infrastructure.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<CategoriesResponseMedia> GetAllCategories();
        public Task<CategoriesResponseMedia> SaveCategory(CategoriesRequestMedia categoriesRequestMedia);
    }
}
