using Microsoft.AspNetCore.Mvc;
using TechShop.Core.Models;

namespace TechShop.Areas.Admin.ViewComponents
{
    public class UploadContentProductViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {
            return View(product);
        }
    }
}
