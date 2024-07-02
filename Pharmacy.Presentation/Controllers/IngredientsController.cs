using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Entities;
using Pharmacy.Web.VWModels.Ingredients;

namespace Pharmacy.Web.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task<IActionResult> Index()
        {
            var ingredients = await _ingredientService.GetAllIngredients();
            return View(ingredients);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateIngredientVWModel ingredient)
        {
            if (ModelState.IsValid)
            {
                await _ingredientService.CreateIngredient(new IngredientDTO()
                {
                    Name = ingredient.Name,
                    Description = ingredient.Description,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var ingredient = await _ingredientService.GetIngredientById((int)id);
            if (ingredient is null)
            {
                return NotFound();
            }

            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _ingredientService.DeleteIngredient(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {

                string errorMessage = "Failed to delete Ingredient becuse it is assoceated with medicine.";
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

            var ingredient = await _ingredientService.GetIngredientById((int)id);
            if(ingredient is null)
            {
                return NotFound();
            }
            var ingredientModel = new EditIngredientVWModel()
            {
                Name = ingredient.Name,
                Id = ingredient.Id,
                Description = ingredient.Description,
            };

            return View(ingredientModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditIngredientVWModel ingredient)
        {
            if (ModelState.IsValid)
            {
                await _ingredientService.UpdateIngredient(new IngredientDTO()
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Description = ingredient.Description,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(ingredient);
        }
    }
}
