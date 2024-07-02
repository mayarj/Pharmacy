using Microsoft.AspNetCore.Mvc;
using Pharmacy.Web.VWModels.Account;
using Pharmacy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Pharmacy.Application.Interfaces;

namespace Pharmacy.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
     

        public AccountController(IUserService userService)
        {
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
            {
                var user = new UserDTO { UserName = model.Email, Email = model.Email };
                var result = await _userService.RegisterAsync(user , model.Password); 
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

       
    }
}
