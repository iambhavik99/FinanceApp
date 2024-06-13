using FinanceApp.Application.Interfaces;
using FinanceApp.Infrastructure.Models.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.API.Controllers
{
    [Route("api/category")]
    [Authorize]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(CategoriesRequestMedia categoriesRequestMedia)
        {
            return Ok(await _categoryService.SaveCategory(categoriesRequestMedia));
        }
    }
}
