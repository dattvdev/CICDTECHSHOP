using Microsoft.AspNetCore.Mvc;
using TechShop.Core.Models;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.ViewComponents
{
    public class UpdateContentImageProductViewComponent : ViewComponent
    {
        private readonly IProductImageRepository _producImageRepository;
        public UpdateContentImageProductViewComponent(IProductImageRepository productImageRepository)
        {
            _producImageRepository = productImageRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {
            List<ProductImage> contentImages = await _producImageRepository.GetListImagesContentByProductId(product.Id);
            ViewBag.ContentImages = contentImages;
            return View(product);
        }
    }
}
