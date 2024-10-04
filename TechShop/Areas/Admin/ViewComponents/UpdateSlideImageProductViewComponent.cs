using Microsoft.AspNetCore.Mvc;
using TechShop.Core.Models;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.ViewComponents
{
    public class UpdateSlideImageProductViewComponent : ViewComponent
    {
        private readonly IProductImageRepository _producImageRepository;
        public UpdateSlideImageProductViewComponent(IProductImageRepository productImageRepository)
        {
            _producImageRepository = productImageRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {
            List<ProductImage> slideImages = await _producImageRepository.GetListImagesSlideByProductId(product.Id);
            ViewBag.SlideImages = slideImages;

            return View(product);
        }
    }
}
