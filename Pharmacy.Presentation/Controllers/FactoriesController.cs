using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Entities;
using Pharmacy.Web.VWModels.Factories;
using Microsoft.EntityFrameworkCore;
namespace Pharmacy.Web.Controllers
{
    public class FactoriesController : Controller
    {
        private readonly IFactoryService _factoryService;

        public FactoriesController(IFactoryService factoryService)
        {
            _factoryService = factoryService;
        }
        
        public async Task<IActionResult> Index()
        {
            var factories = await _factoryService.GetAllFactories();
            return View(factories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFactoryVWModel category)
        {
            if (ModelState.IsValid)
            {
                await _factoryService.CreateFactory(new EntitieFactory()
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

            var category = await _factoryService.GetFactoryById((int)id);
            if (category is null)
            {
                return NotFound();
            }
            var filteredMedicines = await _factoryService.GetAllMedicendelongToFactory((int)id);

            ViewBag.Medicines = filteredMedicines;
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _factoryService.DeleteFactory(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {

                string errorMessage = "Failed to delete factory becuse it is assoceated with medicine.";
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

            var category = await _factoryService.GetFactoryById((int)id);

            var categoryModel = new EditFactoryVWModel()
            {
                Name = category.Name,
                Id = category.Id,
            };

            return View(categoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFactoryVWModel category)
        {
            if (ModelState.IsValid)
            {
                await _factoryService.UpdateFactory(new EntitieFactory()
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
