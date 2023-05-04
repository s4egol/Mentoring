using Catalog.Business.Interfaces;
using Catalog.Mappers;
using Catalog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = (await _categoryService.GetAllAsync())
                .Select(category => category.ToMvc())
                .ToArray();

            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel category)
        {
            await _categoryService.UpdateAsync(category?.ToBusiness());

            return RedirectToAction("Index", "Category");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);

            return RedirectToAction("Index", "Category");
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = (await _categoryService.GetByIdAsync(id))
                .ToMvc();

            return View(category);
        }
    }
}
