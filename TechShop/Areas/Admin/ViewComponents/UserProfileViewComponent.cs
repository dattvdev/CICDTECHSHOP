using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechShop.Areas.Admin.ViewModels;

namespace TechShop.Areas.Admin.ViewComponents
{
    public class UserProfileViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsPrincipal = HttpContext.User;

            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                var userName = claimsPrincipal.FindFirstValue(ClaimTypes.Name);
                var userEmail = claimsPrincipal.FindFirstValue(ClaimTypes.Email);

                var model = new UserProfileViewModel
                {
                    Username = userName,
                    Email = userEmail,
                };

                return View(model);
            }
            return View();
        }
    }
}
