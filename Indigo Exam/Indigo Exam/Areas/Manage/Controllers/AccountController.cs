using Indigo_Exam.Areas.Manage.ViewModels;
using Indigo_Exam.Models;
using Indigo_Exam.Utilities.Enums;
using Indigo_Exam.Utilities.Extentions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Indigo_Exam.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<AppUser> signInManager,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vM)
        {
            if (!ModelState.IsValid) return View();
            if (!vM.Email.CheckEmail())
            {
                ModelState.AddModelError("Email", "Email form is wrong");
                return View();
            }
            AppUser appUser = new AppUser
            {
                Name = vM.Name,
                Surname = vM.Surname,
                Email = vM.Email,
                UserName = vM.UserName,
            };
            IdentityResult result= await _userManager.CreateAsync(appUser,vM.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);
                    return View();
                }
            }
            await _userManager.AddToRoleAsync(appUser,UserRole.Admin.ToString());
            await _signInManager.SignInAsync(appUser,false);
            return RedirectToAction("Index","Home",new {Area=""});
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM vM)
        {
            if (!ModelState.IsValid) return View();
            AppUser appUser = await _userManager.FindByNameAsync(vM.UserNameOrEmail);
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(vM.UserNameOrEmail);
                if (appUser == null)
                {
                    ModelState.AddModelError(String.Empty, "UserNAme or Email is incorrect");
                    return View();
                }
            }
            var result = await _signInManager.PasswordSignInAsync(appUser, vM.Password, vM.IsRemembered, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty, "You are bloced");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "UserNAme or Email is incorrect");
                return View();
            }
            return RedirectToAction("Index", "Home", new { Area = "" });

        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });

        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRole)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            }
            return RedirectToAction("Index", "Home", new { Area = "" });

        }
    }
}
