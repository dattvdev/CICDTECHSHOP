using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.Controllers
{
	public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly TechshopDbContext _context;
        public AuthenticationController(IUserRepository userRepository, IAdminRepository adminRepository, SignInManager<User> signInManager,
            UserManager<User> userManager, TechshopDbContext context)
        {
            _userRepository = userRepository;
            _adminRepository = adminRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

		[Area("Admin")]
        [Route("Admin/Login/")]
        public IActionResult Login()
        {
            return View();
        }

        [Area("Admin")]
        [Route("Admin/Logout/")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Admin/Login");
        }

        [HttpPost]
        [Route("Admin/Login/")]
        public async Task<ActionResult> Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                // Tìm kiếm người dùng theo email
                var user_login = await _context.Users.FirstOrDefaultAsync(l => l.Email.Equals(user.Email));
                
                var adminRole = await _userManager.GetRolesAsync(user_login);
                if (adminRole.Any(i => i.Equals("Admin")))
                {
                    // Check the password for the associated user
                    var result = await _signInManager.PasswordSignInAsync(user_login, user.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        // Successful login, redirect to the admin dashboard
                        return RedirectToAction("Index", "Home", new {Area = "Admin"});
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Invalid password, please enter again.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Admin user not found.");
                }
            }

                return Redirect("/Admin/Login");
        }
    }
}
