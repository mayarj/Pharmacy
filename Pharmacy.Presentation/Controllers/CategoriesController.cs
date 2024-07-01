using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Entities;
using Pharmacy.Web.VWModels.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Pharmacy.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService , ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCatagories();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryVWModel category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategory(new EntitieCategory()
                {
                    Name = category.Name,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryById((int)id);
            if (category is null)
            {
                return NotFound();
            }
            var filteredMedicines = await _categoryService.GetAllMedicenBilongToCatagory((int)id);

            ViewBag.Medicines = filteredMedicines;
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {

                string errorMessage = "Failed to delete category because it is assoceatedd with medicine .";
                TempData["ErrorMessage"] = errorMessage;
                return RedirectToAction(nameof(Index));
            }
           
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryById((int)id);
           
            var categoryModel = new EditCategoryVWModel()
            {
                Name = category.Name,
                Id = category.Id,
            };

            return View(categoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( EditCategoryVWModel category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategory(new EntitieCategory()
                {
                    Id = category.Id,
                    Name = category.Name,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
