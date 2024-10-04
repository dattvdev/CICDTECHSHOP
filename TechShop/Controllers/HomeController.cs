using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using TechShop.Core.Repositories;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using TechShop.Models;
using Order = TechShop.Core.Models.Order;
using Microsoft.AspNetCore.Identity;

namespace TechShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IProductRepository _productRepository;
		private readonly TechshopDbContext _context;
        private readonly UserManager<User> _userManager;
        public HomeController(ILogger<HomeController> logger,IProductRepository productRepository, UserManager<User> userManager, TechshopDbContext context)
        {
            _logger = logger;
			_productRepository = productRepository;
			_context = context;
            _userManager = userManager;
        }

        public ActionResult ListProducts()
        {
            var products = _productRepository.GetProductsWithPrice();
            if (products == null || !products.Any())
            {
                return Content("No products found");  // To check if any products are being retrieved
            }
            return PartialView("_ListProducts", products);
        }

		public ActionResult ListLaptopProducts()
		{
			var products = _productRepository.GetProductsByCategory("Laptops");
			if (products == null || !products.Any())
			{
				return Content("No products found");  // To check if any products are being retrieved
			}
			return PartialView("_LaptopList", products);
		}
		public ActionResult ListPhoneProducts()
		{
			var products = _productRepository.GetProductsByCategory("Phones");
			if (products == null || !products.Any())
			{
				return Content("No products found");  // To check if any products are being retrieved
			}
			var topProducts = products.Take(3).ToList();

			return PartialView("_TopSelling", topProducts);
		}
		public ActionResult ListPCProducts()
		{
			var products = _productRepository.GetProductsByCategory("PCs");
			if (products == null || !products.Any())
			{
				return Content("No products found");  // To check if any products are being retrieved
			}
			var topProducts = products.Take(3).ToList();

			return PartialView("_TopSelling", topProducts);
		}
		public ActionResult ListWatchesProducts()
		{
			var products = _productRepository.GetProductsByCategory("Watches");
			if (products == null || !products.Any())
			{
				return Content("No products found");  // To check if any products are being retrieved
			}
			var topProducts = products.Take(3).ToList();

			return PartialView("_TopSelling", topProducts);
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> HomePage()
		{
			return RedirectToAction("Index", "Home");
		}

		public IActionResult CategoryBar()
		{
			var categories = _context.Categories.ToList();
			return PartialView("_CategoryList", categories);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		[HttpPost]
        public async Task<IActionResult> DeleteOrder(string productColorId)
        {
            var id = _userManager.GetUserId(User);
            var cus = _context.Customers.FirstOrDefault(c => c.UserID == id);
            var gid = Guid.Parse(productColorId);
            var order = _context.Orders.FirstOrDefault(o => o.ProductColorId == gid && o.CustomerId == cus.ID);
            if (order == null)
            {
                return Json(new { success = false, message = "Order not found." });
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            
            var orders = _context.Orders.Include(o => o.ProductColor).ThenInclude(o => o.ProductHardware).ThenInclude(o => o.Product).Where(o => o.CustomerId == cus.ID).ToList();

            return PartialView("_Cart", orders);
        }
        public async Task<IActionResult> Cart()
		{
			var id = _userManager.GetUserId(User);
            var cus = _context.Customers.FirstOrDefault(c => c.UserID == id);
            var orders = _context.Orders.Include(o => o.ProductColor).ThenInclude(o => o.ProductHardware).ThenInclude(o => o.Product).Where(o => o.CustomerId == cus.ID).ToList();

            return PartialView("_Cart", orders);
        }
        [HttpPost]
		public IActionResult Checkout(List<string> productstring)
		{
			var id = _userManager.GetUserId(User);
            var cus = _context.Customers.FirstOrDefault(c => c.UserID == id);
            List<string> products = productstring[0].Split(',').ToList();
			if (products == null || !products.Any())
			{
				return Json(new { success = false, message = "No products selected for checkout." });
			}

			var selectedProducts = new List<Product>();
			var productGuids = new List<Guid>();

			foreach (var productId in products)
			{
				if (Guid.TryParse(productId, out Guid productGuid))
				{
					productGuids.Add(productGuid);
				}
				else
				{
					return Json(new { success = false, message = $"Invalid product ID format: {productId}" });
				}
			}


			List<Order> orders = new List<Order>();
			foreach (var product in productGuids)
			{
				var order = _context.Orders.FirstOrDefault(o => o.CustomerId == cus.ID && o.ProductColorId == product);
				if (order != null)
				{
					orders.Add(order);
				}
			}

            HttpContext.Session.SetString("Orders", JsonConvert.SerializeObject(orders, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));

            return Json(new { success = true, redirectUrl = Url.Action("Index", "Checkout") });
		}

    }
}
