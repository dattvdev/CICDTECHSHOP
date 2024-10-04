using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechShop.Core.Models;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.Controllers.Tracking
{
    [Area("Admin")]
    [Route("Admin/Tracking/{action=Index}/")]
    [Authorize(Roles = "Admin")]
    public class TrackingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TrackingOrder()
        {
            return View();
        }
    }
}
