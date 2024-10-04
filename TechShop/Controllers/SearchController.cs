using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechShop.Core.Data;
using TechShop.Core.Models;

namespace TechShop.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TechshopDbContext _context;
        private readonly UserManager<User> _userManager;
        public SearchController(ILogger<HomeController> logger, TechshopDbContext context, UserManager<User> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(string searchstring, int page = 1)
        {
            int pageSize = 6; 

            var products = _context.Products
                                   .Where(p => p.Name.Contains(searchstring) || string.IsNullOrEmpty(searchstring))
                                   .OrderBy(p => p.Name);
            if (string.IsNullOrEmpty(searchstring))
            {
                products = _context.Products.OrderBy(p => p.Name);
            }

            var paginatedProducts = products.Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            int totalProducts = products.Count();
            ViewBag.TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.SearchString = searchstring;

            return View(paginatedProducts);
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
