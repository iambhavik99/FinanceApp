using FinanceApp.Domain.DBContext;
using FinanceApp.Domain.Models;
using FinanceApp.Infrastructure.Interfaces;
using FinanceApp.Infrastructure.Models.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<CategoriesResponseMedia> GetAllCategories()
        {
            try
            {
                CategoriesResponseMedia categoriesResponseMedia = new CategoriesResponseMedia();
                var response = await _context.Categories.ToListAsync();

                if (response.Count > 0)
                {
                    categoriesResponseMedia.items = response
                        .Where(x => x.name != "INITIAL_BALANCE")
                        .Select(x => new Category { id = x.id, name = x.name })
                        .ToList();
                }

                return categoriesResponseMedia;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoriesResponseMedia> SaveCategory(CategoriesRequestMedia categoriesRequestMedia)
        {
            try
            {
                Categories category = new Categories();
                category.id = new Guid();
                category.name = categoriesRequestMedia.categoryName;

                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                return await GetAllCategories();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
