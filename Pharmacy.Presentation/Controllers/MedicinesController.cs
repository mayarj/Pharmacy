using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Entities;
using Pharmacy.Web.VWModels.Medicines;
using Pharmacy.Web.VWModels.MedicineIngredient;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Pharmacy.Web.Controllers
{
    public class MedicinesController : Controller
    {
        private readonly IMedicineService _medicineService;
        private readonly ICategoryService _categoryService;
        private readonly IIngredientService _ingredientService;
        private readonly IFactoryService _factoryService;
        private readonly IMedicineIngredientService _medicineIngredientService;
        public MedicinesController(IMedicineIngredientService medicineIngredientService, IMedicineService medicineService, ICategoryService categoryService, IIngredientService ingredientService, IFactoryService factoryService)
        {
            _medicineIngredientService = medicineIngredientService;
            _medicineService = medicineService;
            _categoryService = categoryService;
            _factoryService = factoryService;
            _ingredientService = ingredientService;
        }
        public async Task<IActionResult> Index()
        {
            var medicines = await _medicineService.GetAllMedicines();
            return View(medicines);
        }
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCatagories();
            var categoriesSelect = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var ingredients = await _ingredientService.GetAllIngredients();
            var ingredientsSelect = ingredients.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            }).ToList();

            var factories = await _factoryService.GetAllFactories();
            var factoriesSelect = factories.Select(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.Name
            }).ToList();

            ViewBag.CategoryId = new SelectList(categoriesSelect, "Value", "Text");
            ViewBag.ActiveSubstanceId = new SelectList(ingredientsSelect, "Value", "Text");
            ViewBag.FactoryId = new SelectList(factoriesSelect, "Value", "Text");
            return View();
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMedicineVWModel medicine)
        {
            if (ModelState.IsValid)
            {
                await _medicineService.CreateMedicine(new MedicineDTO()
                {
                    Name = medicine.Name,
                    Description = medicine.Description,
                    CategoryId = medicine.CategoryId,
                    Dose = medicine.Dose,
                    ActiveSubstanceId = medicine.ActiveSubstanceId,
                    InStock = medicine.InStock,
                    FactoryId = medicine.FactoryId,
                    TradeName = medicine.TradeName,
                });


                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryService.GetAllCatagories();
            var categoriesSelect = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var ingredients = await _ingredientService.GetAllIngredients();
            var ingredientsSelect = ingredients.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            }).ToList();

            var factories = await _factoryService.GetAllFactories();
            var factoriesSelect = factories.Select(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.Name
            }).ToList();

            ViewBag.CategoryId = new SelectList(categoriesSelect, "Value", "Text");
            ViewBag.ActiveSubstanceId = new SelectList(ingredientsSelect, "Value", "Text");
            ViewBag.FactoryId = new SelectList(factoriesSelect, "Value", "Text");
            return View(medicine);
        }
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var medicine = await _medicineService.GetMedicineById((int)id);
            if (medicine is null)
            {
                return NotFound();
            }
            var categories = await _categoryService.GetAllCatagories();
            var categoriesSelect = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var ingredients = await _medicineService.GetIngredients(medicine.Id);
            var ingredientsSelect = ingredients.Select(i => new SelectListItem
            {
                Value = i.IngredientId.ToString(),
                Text = i.Name
            }).ToList();

            var factories = await _factoryService.GetAllFactories();
            var factoriesSelect = factories.Select(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.Name
            }).ToList();

            ViewBag.CategoryId = new SelectList(categoriesSelect, "Value", "Text");
            ViewBag.ActiveSubstanceId = new SelectList(ingredientsSelect, "Value", "Text");
            ViewBag.FactoryId = new SelectList(factoriesSelect, "Value", "Text");
            var medicineModel = new EditMedicineVWModel()
            {
                Id = medicine.Id,
                Name = medicine.Name,
                ActiveSubstanceId = (int)medicine.ActiveSubstanceId,
                CategoryId = medicine.CategoryId,
                Description = medicine.Description,
                Dose = medicine.Dose,
                FactoryId = medicine.FactoryId,
                InStock = medicine.InStock,
                TradeName = medicine.TradeName,
            };
            return View(medicineModel);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMedicineVWModel medicine)
        {
            if (ModelState.IsValid)
            {

                await _medicineService.UpdateMedicine(new MedicineDTO()
                {
                    Id = medicine.Id,
                    Name = medicine.Name,
                    Description = medicine.Description,
                    CategoryId = medicine.CategoryId,
                    Dose = medicine.Dose,
                    ActiveSubstanceId = medicine.ActiveSubstanceId,
                    InStock = medicine.InStock,
                    FactoryId = medicine.FactoryId,
                    TradeName = medicine.TradeName,
                });

                return RedirectToAction(nameof(Index));
            }



            var categories = await _categoryService.GetAllCatagories();
            var categoriesSelect = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var ingredients = await _medicineService.GetIngredients(medicine.Id);
            var ingredientsSelect = ingredients.Select(i => new SelectListItem
            {
                Value = i.IngredientId.ToString(),
                Text = i.Name
            }).ToList();

            var factories = await _factoryService.GetAllFactories();
            var factoriesSelect = factories.Select(f => new SelectListItem
            {
                Value = f.Id.ToString(),
                Text = f.Name
            }).ToList();

            ViewBag.CategoryId = new SelectList(categoriesSelect, "Value", "Text");
            ViewBag.ActiveSubstanceId = new SelectList(ingredientsSelect, "Value", "Text");
            ViewBag.FactoryId = new SelectList(factoriesSelect, "Value", "Text");
            return View(medicine);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _medicineService.DeleteMedicine(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                string errorMessage = "Failed to delete medicine .";
                TempData["ErrorMessage"] = errorMessage;
                return RedirectToAction(nameof(Index));
            }
        }
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var medicine = await _medicineService.GetMedicineById((int)id);
            if (medicine is null)
            {
                return NotFound();
            }
            var filteredIngredients = await _medicineService.GetIngredients((int)id);


            ViewBag.Ingredients = filteredIngredients;


            return View(medicine);
        }
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> EditIngredients(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var medicine = await _medicineService.GetMedicineById((int)id);
            if (medicine is null)
            {
                return NotFound();
            }
            var filteredIngredients = await _medicineService.GetIngredients((int)id);
            ViewBag.FilteredIngredients = filteredIngredients;
            var ingredients = await _ingredientService.GetAllIngredients();
            var ingredientsSelect = ingredients.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            }).ToList();

            ViewBag.Ingredients = new SelectList(ingredientsSelect, "Value", "Text");
            ViewBag.MedicineId = (int)id;
            return View();
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddIngredient([FromRoute] int id, AddIngredientToMedicineVWModel ingredientToMedicine)
        {

            if (ModelState.IsValid)
            {
                await _medicineService.AddIngrediantToMedicen(new Domain.Aggregation.MedicineIngredientDTO()
                {
                    IngredientId = ingredientToMedicine.IngredientId,
                    MedicineId = ingredientToMedicine.MedicineId,
                    Ratio = ingredientToMedicine.Ratio,
                });
                return RedirectToAction(nameof(EditIngredients), new { id = id });
            }

            return RedirectToAction(nameof(EditIngredients), new { id = id });

        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteIngredient(int ingredientId, int medicineId)
        {

            if (ModelState.IsValid)
            {
                await _medicineIngredientService.Delete(medicineId, ingredientId);

                return RedirectToAction(nameof(EditIngredients), new { id = medicineId });
            }

            return RedirectToAction(nameof(EditIngredients), new { id = medicineId });

        }
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> EditIngredient(int ingredientId, int medicineId)
        {
            var medicineIngredientDTO = await _medicineService.GetMedicineIngredientDTO(medicineId, ingredientId);
            if(medicineIngredientDTO is null)
            {
                return NotFound();
            }
            var ingredient = new EditIngredientInMedicineVWModel()
            {
                Id = medicineIngredientDTO.Id,
                IngredientId = medicineIngredientDTO.IngredientId,
                MedicineId = medicineIngredientDTO.MedicineId,
                Ratio = medicineIngredientDTO.Ratio,
                IngredientName = medicineIngredientDTO.Name,
            };
            return View(ingredient);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditIngredient(EditIngredientInMedicineVWModel model)
        {

            if (ModelState.IsValid)
            {
                await _medicineIngredientService.Update(new Domain.Aggregation.MedicineIngredientDTO()
                {
                    Id = model.Id,
                    IngredientId = model.IngredientId,
                    MedicineId =  model.MedicineId,
                    Ratio=model.Ratio,
                    Name = model.IngredientName,
                });
                
                return RedirectToAction(nameof(EditIngredients), new { id = model.MedicineId });
            }

            return RedirectToAction(nameof(EditIngredient), new { ingredientId = model.IngredientId, medicineId = model.MedicineId });

        }
    }
}
