using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ShaTask.Models;
using ShaTask.Needs;
using ShaTask.ViewModel;

namespace ShaTask.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public LoginController(UserManager<IdentityUser> userManager , SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM cred)
        { 
            if (ModelState.IsValid) {

                var user = await _userManager.FindByEmailAsync(cred.Email);
                if (user != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(user, cred.Password);
                    if (found)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: cred.IsPresistent);
                        if (await _userManager.IsInRoleAsync(user, Roles.Adminstrator.ToString()))
                        {
                            return RedirectToAction("Index", "Invoices");
                        }

                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                        }
                    }

                }
            ModelState.AddModelError("", "Invalid Email or password");
            return View(cred);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }
    }
}
