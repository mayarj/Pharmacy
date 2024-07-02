using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmacy.Web.VWModels.Prescriptions;
using Pharmacy.Domain.Aggregation;
using Microsoft.AspNetCore.Authorization;

namespace WebTest.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IMedicineService _medicineService;
        private readonly IPrescriptionMedicineService _prescriptionMedicineService;
        private readonly IPatientService _patientService;
        private readonly IUserService _userService;
        public PrescriptionsController(IPrescriptionService prescriptionService, IPatientService patientService, IPrescriptionMedicineService prescriptionMedicineService, IMedicineService medicineService, IUserService userService)
        {
            _userService = userService;
            _patientService = patientService;
            _medicineService = medicineService;
            _prescriptionMedicineService = prescriptionMedicineService;
            _prescriptionService = prescriptionService;
        }
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Index()
        {
            var prescriptions = await _prescriptionService.GetAllPrescription();
            return View(prescriptions);
        }
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> MyPrescriptions()
        {
            var user = await _userService.GetLoggedInUser(HttpContext);
            Console.WriteLine(user.Id);
            Console.WriteLine(user.PatientId);
            if (user.PatientId is null)
            {
                return NotFound();
            }
            var prescriptions = await _patientService.GetPrescriptions((int)user.PatientId);
            return View(prescriptions);
        }
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(int id)
        {
            var prescription = new CreatePrescriptionVWModel()
            {
                PatientId = id,
            };
            return View(prescription);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePrescriptionVWModel prescription)
        {
            if (ModelState.IsValid)
            {
                var newPrescription = await _prescriptionService.CreatePrescription(new PrescriptionDTO()
                {
                    Name = prescription.Name,
                    Note = prescription.Note,
                    PatientId = prescription.PatientId,
                });
                return RedirectToAction("Details", "Prescriptions", new { id = newPrescription.Id });
            }
            return View(prescription);
        }
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var prescription = await _prescriptionService.GetPrescriptionById((int)id);
            if (prescription is null)
            {
                return NotFound();
            }

            var filteredMedicines = await _prescriptionMedicineService.GetmedicineByPrescription(prescription.Id);
            var prescriptionModel = new EditPrescriptionVWModel()
            {
                Id = prescription.Id,
                Name = prescription.Name,
                Note = prescription.Note,
                PatientId = prescription.PatientId,
                Medicines = filteredMedicines
                                    .Select(i => new MedicineInfo()
                                    {
                                        Id = i.MedicineId,
                                        Category = i.Medicine.CategoryName,
                                        Count = i.Count,
                                        Dose = i.Medicine.Dose,
                                        Name = i.Medicine.Name,
                                        TradeName = i.Medicine.TradeName,
                                    }).ToList(),
            };
            var medicines = await _medicineService.GetAllMedicines();
            var medicinesSelect = medicines.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            }).ToList();

            ViewBag.Medicines = new SelectList(medicinesSelect, "Value", "Text");
            return View(prescriptionModel);
        }
        [Authorize(Policy = "UserAndAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditPrescriptionVWModel prescription)
        {
            if (ModelState.IsValid)
            {
                await _prescriptionService.UpdatePrescription(new PrescriptionDTO()
                {
                    Id = prescription.Id,
                    Note = prescription.Note,
                    Name = prescription.Name,
                    PatientId = prescription.PatientId,
                });
                return RedirectToAction("Details", "Prescriptions", new { id = prescription.PatientId });
            }
            var medicines = await _medicineService.GetAllMedicines();
            var medicinesSelect = medicines.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Name
            }).ToList();

            ViewBag.Medicines = new SelectList(medicinesSelect, "Value", "Text");
            return View(prescription);
        }
        [Authorize(Policy = "UserAndAdmin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var prescription = await _prescriptionService.GetPrescriptionById((int)id);
            if (prescription is null)
            {
                return NotFound();
            }

            var filteredMedicines = await _prescriptionMedicineService.GetmedicineByPrescription(prescription.Id);
            var prescriptionModel = new DetailsPrescriptionVWModel()
            {
                Id = prescription.Id,
                Name = prescription.Name,
                Note = prescription.Note,
                PatientId = prescription.PatientId,
                Patient = prescription.PatientName,
                Medicines = filteredMedicines
                                    .Select(i => new MedicineInfo()
                                    {
                                        Id = i.Id,
                                        Category = i.Medicine.CategoryName,
                                        Count = i.Count,
                                        Dose = i.Medicine.Dose,
                                        Name = i.Medicine.Name,
                                        TradeName = i.Medicine.TradeName,
                                    }).ToList(),
            };

            return View(prescriptionModel);
        }
        [Authorize(Policy = "UserAndAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMedicine(AddMedicineToPrescriptionVWModel medicine)
        {

            if (ModelState.IsValid)
            {
                await _prescriptionMedicineService.CreatePrescriptionMedicineDTO(new Pharmacy.Domain.Aggregation.PrescriptionMedicineDTO()
                {
                    Count = medicine.Count,
                    MedicineId = medicine.MedicineId,
                    PrescriptionId = medicine.PrescriptionId,
                });
            }
            return RedirectToAction(nameof(Edit), new { id = medicine.PrescriptionId });

        }

        [Authorize(Policy = "UserAndAdmin")]

        public async Task<IActionResult> EditMedicine(int medicineId, int descriptionId)
        {
            var descriptionMedicine = await _prescriptionMedicineService.GetPrescriptionMedicineById(medicineId, descriptionId);

            if (descriptionMedicine is null)
            {
                return NotFound();
            }
            var descriptionMedicineModel = new EditMedicineInPrescriptionVWModel()
            {
                Id = descriptionMedicine.Id,
                MedicineId = descriptionMedicine.MedicineId,
                Count = descriptionMedicine.Count,
                PrescriptionId = descriptionMedicine.PrescriptionId,
            };
            ViewBag.Medicine = await _medicineService.GetMedicineById(medicineId);
            return View(descriptionMedicineModel);

        }
        [Authorize(Policy = "UserAndAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMedicine(EditMedicineInPrescriptionVWModel model)
        {
            if (ModelState.IsValid)
            {
                var descriptionMedicine = new PrescriptionMedicineDTO
                {
                    Id = model.Id,
                    MedicineId = model.MedicineId,
                    PrescriptionId = model.PrescriptionId,
                    Count = model.Count,
                };
                await _prescriptionMedicineService.UpdatePrescriptionMedicineDTO(descriptionMedicine);
                return RedirectToAction(nameof(Edit), new { id = model.PrescriptionId });
            }
            return RedirectToAction(nameof(EditMedicine), new { medicineId = model.MedicineId, descriptionId = model.PrescriptionId });

        }
        [Authorize(Policy = "UserAndAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _prescriptionService.DeletePrescription(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {

                string errorMessage = "Failed to delete Prescriptionbecuse it is assoceated with medicine .";
                TempData["ErrorMessage"] = errorMessage;
                return RedirectToAction(nameof(Index));
            }
        }
        [Authorize(Policy = "UserAndAdmin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMedicine(int medicineId, int descriptionId)
        {

            if (ModelState.IsValid)
            {
                await _prescriptionMedicineService.DeletePrescriptionMedicineDTO(medicineId, descriptionId);
            }

            return RedirectToAction(nameof(Edit), new { id = descriptionId });

        }
    }
}
