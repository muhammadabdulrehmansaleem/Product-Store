using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var users =_userManager.Users.ToList();
            var emails = users.Select(u => u.Email).ToList();
            return View("~/Views/Admin/Index.cshtml",emails);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string email=null)
        {
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to delete user.";
                        return RedirectToAction("Index", "Admin");
                    }

                }
            }
            return View("Index");
        }
    }
}
