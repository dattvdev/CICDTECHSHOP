using Microsoft.AspNetCore.Mvc;
using TechShop.Core.Models;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.ViewComponents
{
    public class UpdateHardwareProductViewComponent : ViewComponent
    {
        private readonly IProductHardwareRepository _productHardwareRepository;
        public UpdateHardwareProductViewComponent(IProductHardwareRepository productHardwareRepository)
        {
            _productHardwareRepository = productHardwareRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {
            List<ProductHardware> productHardwares = await _productHardwareRepository.GetListProductHardwareByProductId(product.Id);
            ViewBag.ProductHardwares = productHardwares;

            return View(product);
        }
    }
}
