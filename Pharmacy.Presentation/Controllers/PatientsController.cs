using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Interfaces;
using Pharmacy.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pharmacy.Web.VWModels.Patients;
namespace Pharmacy.Web.Controllers
{
    public class PatientsController : Controller
    {

        private readonly IPatientService _patientService;
        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        public async Task<IActionResult> Index()
        {
            var patients = await _patientService.GetAllPatients();
            return View(patients);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePatientVWModel patient)
        {
            if (ModelState.IsValid)
            {
                await _patientService.CreatePatient(new PatientDTO()
                {
                    Address = patient.Address,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    PhoneNumber = patient.PhoneNumber,
                });
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _patientService.GetPatientById(id);
            if (patient is null)
            {
                return NotFound();
            }

            var patientModel = new EditPatientVWModel
            {
                Id = id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber,
            };

            return View(patientModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditPatientVWModel patient)
        {


            if (ModelState.IsValid)
            {
                if (patient is null)
                {
                    return NotFound();
                }
                await _patientService.UpdatePatient(new PatientDTO()
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Address = patient.Address,
                    PhoneNumber = patient.PhoneNumber,
                });

                return RedirectToAction("Index");
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _patientService.DeletePatient(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var patient = await _patientService.GetPatientById((int)id);
            if (patient is null)
            {
                return NotFound();
            }
            var filteredPrescriptions = await _patientService.GetPrescriptions((int)id);
            var patientModel = new DetailsPatientVWModel()
            {
                Address = patient.Address,
                FirstName = patient.FirstName,
                Id = patient.Id,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Prescriptions = filteredPrescriptions.Select(i => new PrescriptionInfo()
                {
                    Id = i.Id,
                    Name = i.Name,
                    PatientId = i.Id,
                    Note = i.Note,
                }).ToList(),
            };
            return View(patientModel);
        }
    }
}
