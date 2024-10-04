using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Repositories;

namespace TechShop.Areas.AreaCollaborator.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly TechshopDbContext _context;

		public AuthenticationController(SignInManager<User> signInManager,
			UserManager<User> userManager, TechshopDbContext context)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		[Area("AreaCollaborator")]
		[Route("AreaCollaborator/Login/")]
		public IActionResult Login()
		{
			return View();
		}

		[Area("AreaCollaborator")]
		[Route("AreaCollaborator/Logout/")]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Redirect("/AreaCollaborator/Login");
		}

		[HttpPost]
        [Area("AreaCollaborator")]
        [Route("AreaCollaborator/Login/")]
		public async Task<ActionResult> Login(UserLogin user)
		{
			if (ModelState.IsValid)
			{
				// Tìm kiếm người dùng theo email
				var user_login = await _context.Users.FirstOrDefaultAsync(l => l.Email.Equals(user.Email));

				if(user_login == null)
				{
                    ModelState.AddModelError("Email", "Collaborator user not found.");
					return View(user);
                }

				var collabRole = await _userManager.GetRolesAsync(user_login);
				if (collabRole.Any(i => i.Equals("Collaborator")))
				{
					// Check the password for the associated user
					var result = await _signInManager.PasswordSignInAsync(user_login, user.Password, isPersistent: false, lockoutOnFailure: false);
					if (result.Succeeded)
					{
						// Successful login, redirect to the admin dashboard
						return Redirect("/AreaCollaborator/Home");
					}
					else
					{
						ModelState.AddModelError("Password", "Invalid password, please enter again.");
					}
				}
				
			}

			return View(user);
		}

	}
}
