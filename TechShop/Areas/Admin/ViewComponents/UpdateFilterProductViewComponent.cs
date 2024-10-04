using Microsoft.AspNetCore.Mvc;
using TechShop.Core.Models;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.ViewComponents
{
    public class UpdateFilterProductViewComponent : ViewComponent
    {
        private readonly IProductFilterRepository _producFiltertRepository;
        private readonly IFilterRepository _filterRepository;
        public UpdateFilterProductViewComponent(IProductFilterRepository producFiltertRepository, IFilterRepository filterRepository)
        {
            _producFiltertRepository = producFiltertRepository;
            _filterRepository = filterRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {
            var filters = await _filterRepository.GetFilterByCategoryId(product.CategoryId);
            List<Guid?> filtersSelectedId = await _producFiltertRepository.GetProductFilterIds(product.Id);

            ViewBag.Filters = filters;
            ViewBag.SelectedFilterIds = filtersSelectedId;

            return View(product);
        }
    }
}
