using Microsoft.AspNetCore.Mvc;
using TechShop.Core.Data;
using TechShop.Core.Repositories;

namespace TechShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
		private readonly TechshopDbContext _context;
		public ProductController(IProductRepository productRepository, TechshopDbContext context)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("Product/Detail/{urlSlug}")]
        public async Task<IActionResult> Detail(string urlSlug) 
        {
            var product = await _productRepository.GetProductByUrlSlug(urlSlug);
            if (product == null) { 
                return NotFound();
            }
            return View(product);
        }
    }
}
