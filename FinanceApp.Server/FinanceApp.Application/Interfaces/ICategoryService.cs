using FinanceApp.Infrastructure.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<CategoriesResponseMedia> GetAllCategories();
        public Task<CategoriesResponseMedia> SaveCategory(CategoriesRequestMedia categoriesRequestMedia);
    }
}
