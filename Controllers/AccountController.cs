using DeliciousMvC.AccounViewModel;
using DeliciousMvC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliciousMvC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _rolemanager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rolemanager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser()
            {
                Name = register.Name,
                Email = register.Email,
                UserName = register.UserName,
                Surname = register.Surname,

            };
            IdentityResult result = await _userManager.CreateAsync(user,register.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
            } 
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user =await _userManager.FindByEmailAsync(login.Email);
            if(user== null)
            {
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult sign= await _signInManager.PasswordSignInAsync(user,login.Password,true,false);
            if (!sign.Succeeded)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        //public async Task< IActionResult> AddRole()
        //{
        //    string role = "SuperAdmin";
        //    IdentityRole role =new IdentityRole("SuperAdmin")
        //    await _rolemanager.CreateAsync(role)
        //}
    }
}
