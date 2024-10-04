using Microsoft.AspNetCore.Mvc;
using TechShop.Core.Models;

namespace TechShop.Areas.Admin.ViewComponents
{
    public class UpdateMainImageProductViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {
            return View(product);
        }
    }
}
