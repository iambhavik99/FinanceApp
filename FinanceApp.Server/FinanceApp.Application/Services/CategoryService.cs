using FinanceApp.Application.Interfaces;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoriesResponseMedia> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<CategoriesResponseMedia> SaveCategory(CategoriesRequestMedia categoriesRequestMedia)
        {
            return await _categoryRepository.SaveCategory(categoriesRequestMedia);
        }
    }
}
