using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Authentication.Login;
using WebApplication1.Models.Authentication.Signup;

namespace WebApplication1.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<IdentityUser> userManager,
                               SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration; 
        }
    
        public IActionResult Register()
        {
            return View("~/Views/Authentication/Signup.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ViewBag.EmailAlreadyExists="Email is already registered. Please login or use a different email.";
                    return View("~/Views/Authentication/Signup.cshtml");
                }
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return RedirectToAction("Register", "Authentication");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View("~/Views/Authentication/Login.cshtml");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login user)
        {
            if (ModelState.IsValid)
            {
               
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    var identityUser = await _userManager.FindByEmailAsync(user.Email);

                    if (identityUser != null)
                    {
                        var isAdmin = await _userManager.IsInRoleAsync(identityUser, "Admin");

                        if (isAdmin)
                        {
                            Console.WriteLine("User is an admin.");

                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            Console.WriteLine("User is not an admin.");
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index","Home");
        }
    }
}
