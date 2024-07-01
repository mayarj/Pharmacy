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
        public async Task<IActionResult> Create(CreateFactoryVWModel factory)
        {
            if (ModelState.IsValid)
            {
                await _factoryService.CreateFactory(new EntitieFactory()
                {
                    Name = factory.Name,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(factory);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var factory = await _factoryService.GetFactoryById((int)id);
            if (factory is null)
            {
                return NotFound();
            }
            var filteredMedicines = await _factoryService.GetAllMedicendelongToFactory((int)id);

            ViewBag.Medicines = filteredMedicines;
            return View(factory);
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

            var factory = await _factoryService.GetFactoryById((int)id);
            if (factory is null)
            {
                return NotFound();
            }

            var factoryModel = new EditFactoryVWModel()
            {
                Name = factory.Name,
                Id = factory.Id,
            };

            return View(factoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFactoryVWModel factory)
        {
            if (ModelState.IsValid)
            {
                await _factoryService.UpdateFactory(new EntitieFactory()
                {
                    Id = factory.Id,
                    Name = factory.Name,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(factory);
        }
    }
}
