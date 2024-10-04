using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using Newtonsoft.Json;
using TechShop.Core.Data;
using TechShop.Core.Models;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace TechShop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TechshopDbContext _context;
        private readonly UserManager<User> _userManager;
		private readonly PayOS _payOS;

		public CheckoutController(ILogger<HomeController> logger, TechshopDbContext context, UserManager<User> userManager, PayOS payOS)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
			_payOS = payOS;
		}
        public IActionResult Index()
        {
            // Đọc danh sách orders từ Session
            var ordersData = HttpContext.Session.GetString("Orders");
            if (ordersData == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var orders = JsonConvert.DeserializeObject<List<Order>>(ordersData);
            //write add product and customer for each customer
            foreach (var order in orders)
            {
                order.Customer = _context.Customers.FirstOrDefault(c => c.ID == order.CustomerId);
                order.ProductColor = _context.ProductColors.FirstOrDefault(p => p.Id == order.ProductColorId);
                order.ProductColor.ProductHardware = _context.ProductHardwares.FirstOrDefault(p => p.Id == order.ProductColor.ProductHardwareId);
                order.ProductColor.ProductHardware.Product = _context.Products.FirstOrDefault(p => p.Id == order.ProductColor.ProductHardware.ProductId);
            }
            TempData["Orders"] = JsonConvert.SerializeObject(orders, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            HttpContext.Session.Remove("Orders");
            return View(orders);
        }
        [HttpPost]
        public IActionResult AddToCart(Guid colorId, int quantity)
        {
                
            var ProductColor = _context.ProductColors.FirstOrDefault(p => p.Id == colorId);
            var id = _userManager.GetUserId(User);
            var cus = _context.Customers.FirstOrDefault(c => c.UserID == id);
            var existOrder = _context.Orders.FirstOrDefault(o => o.CustomerId == cus.ID && o.ProductColorId == colorId);
            if (existOrder != null)
            {
                if (existOrder.Quanitity + quantity > ProductColor.Quantity)
                {
                    return Json(new { success = false, message = "Not enough quantity" });
                }   
                existOrder.Quanitity += quantity;
                _context.Orders.Update(existOrder);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            if (quantity > ProductColor.Quantity)
            {
                return Json(new { success = false, message = "Not enough quantity" });
            }
            var order = new Order
            {
                CustomerId = cus.ID,
                ProductColorId = colorId,
                Quanitity = quantity,
                CreatedAt = DateTime.Now
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return Json(new { success = true });
        }


        [HttpPost]
        public async Task<IActionResult> Payment(string Name, string tel, string provinceName, string districtName, string wardName, string address, string note, string payment, string orderInput)
        {
            var ordersData = TempData["Orders"] as string;
            if (ordersData == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var orders = JsonConvert.DeserializeObject<List<Order>>(ordersData);
            var ordersDE = JsonConvert.DeserializeObject<List<Order>>(orderInput);
            // Validate required fields
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(tel) || string.IsNullOrEmpty(provinceName)
                || string.IsNullOrEmpty(districtName) || string.IsNullOrEmpty(note) || string.IsNullOrEmpty(wardName)
                || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(payment))
            {
                ModelState.AddModelError(string.Empty, "Please fill in all required fields.");
                return RedirectToAction("Index", "Checkout", orders);
            }


            double total = 0;
            List<InvoiceProductColors> colors = new List<InvoiceProductColors>();
			foreach (var order in orders)
            {
                var pc = _context.ProductColors.FirstOrDefault(p => p.Id == order.ProductColorId);
                if (order.Quanitity > pc.Quantity)
                {
                    
                    string CheckoutError = "Not enough quantity";
					TempData["CheckoutError"] = CheckoutError;
					ModelState.AddModelError(string.Empty, "Not enough quantity");
                    return RedirectToAction("Index", "Home");
                }
                colors.Add(new InvoiceProductColors
                {
                    ProductColorsId = order.ProductColorId,
                    Quantity = order.Quanitity,
                    CreatedAt = DateTime.Now,
                    ProductPrice = order.ProductColor.Price
                });
                pc.Quantity -= order.Quanitity;
                _context.ProductColors.Update(pc);
                total += order.Quanitity * order.ProductColor.Price;
         
            }
            
            var id = _userManager.GetUserId(User);
            var cus = _context.Customers.AsNoTracking().FirstOrDefault(c => c.UserID == id);
            string Ads = provinceName + ", " + districtName + ", " + wardName + ", " + address;
            var invoice = new Invoice
            {
                CustomerId =cus.ID,
                Status = 1,
                MethodPaymment = payment,
                CreatedAt = DateTime.Now,
                Name = Name,
                Telephone = tel,
                Address = Ads,
                Note = note,
                InvoiceProducts = colors,
                Total = total
            };
         
            if (payment=="Online")
            {
				int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
				//ItemData item = new ItemData("Mì tôm hảo hảo ly", 1, 1000);
				var newProducts = ordersDE.Select(c => new
				{
					name = c.ProductColor.ProductHardware.Product.Name,   // Replace with the actual property you want from c
					quantity = c.Quanitity,   // Add more properties as needed
					price = c.ProductColor.Price * c.Quanitity * 23500 // Example of a custom property
                }).ToList();
				List<ItemData> items = new List<ItemData>();
				items = newProducts.Select(np => new ItemData(np.name, (int)np.quantity, (int)np.price)).ToList();
				//items.Add(item);
				PaymentData paymentData = new PaymentData(orderCode, 2000, "Thanh toan don hang", items, "http://" + Request.Host + "/cancel", "http://" + Request.Host + "/Checkout/Payment", null,Name,null,invoice.Telephone,invoice.Address);

				CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                foreach (var order in ordersDE)
                {
                    var item = _context.Orders.FirstOrDefault(o => o.ProductColorId == order.ProductColorId && o.CustomerId == cus.ID);
                    _context.Orders.Remove(item);
                }
                _context.invoices.Add(invoice);
                _context.SaveChanges();
                return Redirect(createPayment.checkoutUrl);
			}

            foreach (var order in orders)
            {
                var item = _context.Orders.FirstOrDefault(o => o.ProductColorId == order.ProductColorId && o.CustomerId == cus.ID);
                _context.Orders.Remove(item);
            }
            _context.invoices.Add(invoice);
            _context.SaveChanges();
            return View();
        }

    } 
}
