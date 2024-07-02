using Microsoft.AspNetCore.Mvc;
using Pharmacy.Web.VWModels.Account;
using Pharmacy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Pharmacy.Application.Interfaces;
using Pharmacy.Web.VWModels.Patients;

namespace Pharmacy.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPatientService _patientService;

        public AccountController(IUserService userService, IPatientService patientService)
        {
            _patientService = patientService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVWModel model)
        {
            if (ModelState.IsValid)
            {var patient = await _patientService.CreatePatient(new PatientDTO
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = Int32.Parse(model.PhoneNumber),
                    
                });
                var user = new UserDTO { UserName = model.Email, Email = model.Email , PatientId = patient.Id};
                var result = await _userService.RegisterAsync(user, model.Password);
                
                return RedirectToAction("Index", "Home");

            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVWModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginAsync(model.Email, model.Password);


                return RedirectToAction("Index", "Home");


            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet("Account/")]
        [HttpGet("Account/Index")]
        [HttpGet("Account/Profile")]
        public async Task<IActionResult> Profile()
        {
            var email = User.Identity.Name;
            var userDto = await _userService.GetUserAsync(email);
            if(userDto is null)
            {
                return NotFound();
            }
            var patient = await _patientService.GetPatientById((int)userDto.PatientId);
            if (patient is null)
            {
                return NotFound();
            }
            var filteredPrescriptions = await _patientService.GetPrescriptions(patient.Id);
            var model = new ProfileVWModel
            {
                Email = userDto.Email,
                Patient = new DetailsPatientVWModel()
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

                }
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var email = User.Identity.Name;
            var userDto = await _userService.GetUserAsync(email);
            if (userDto is null)
            {
                return NotFound();
            }

            var patient = await _patientService.GetPatientById((int)userDto.PatientId);
            if (patient is null)
            {
                return NotFound();
            }
            var model = new EditProfileVWModel
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Address = patient.Address,
                PhoneNumber = patient.PhoneNumber.ToString(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileVWModel model)
        {
            if (ModelState.IsValid)
            {
                var userDto = await _userService.GetUserAsync(User.Identity.Name);
                await _patientService.UpdatePatient(new PatientDTO()
                {
                    Id = (int)userDto.PatientId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = Int32.Parse(model.PhoneNumber),
                });

                return RedirectToAction("Index");

            }

            return View(model);
        }


    }
}
