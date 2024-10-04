using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TechShop.Areas.Admin.ViewModels;
using TechShop.Core.Models;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.ViewComponents
{
    public class UpdateInformationProductViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;

        public UpdateInformationProductViewComponent(ICategoryRepository categoryRepository, IBrandRepository brandRepository)
        {
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name");
            ViewBag.Brands = new SelectList(_brandRepository.GetAllBrands(), "Id", "Name");

            return View(product);
        }
    }
}
