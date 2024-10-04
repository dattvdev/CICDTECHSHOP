using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using TechShop.Core.Data;
using TechShop.Core.Models;

namespace TechShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly TechshopDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICompositeViewEngine _viewEngine;
        public CategoryController(ICompositeViewEngine viewEngine, IServiceProvider serviceProvider, TechshopDbContext context)
        {
            _viewEngine = viewEngine;
            _serviceProvider = serviceProvider;
            _context = context;
        }
        public IActionResult Index(string category)
        {
            var cate = _context.Categories.FirstOrDefault(c => c.UrlSlug == category);
            if (cate == null)
            {
                return Content("Category not found");
            }
            //get list specification by category id inclue list filter by specification id
            var specifications = _context.Specifications.Include(s => s.Filters).Where(s => s.CategoryId == cate.Id).ToList();

            return View(specifications);
        }

        [HttpPost]
        public IActionResult Filter(List<string> ids, int page = 1, string category="")
        {
       
            var listid = ids.Select(id => new Guid(id)).ToList();
            var list = _context.ProductFilters;
            var categoryy = _context.Categories.FirstOrDefault(c => c.UrlSlug == category);
            if (ids == null || ids.Count == 0)
            {
                var productss = _context.Products.Where(o => o.CategoryId == categoryy.Id).ToList();
                int pageSizee = 6;
                var paginatedProductss = productss.Skip((page - 1) * pageSizee)
                                                .Take(pageSizee)
                                                .ToList();

                int totalProductss = productss.Count();
                int totalPagess = (int)Math.Ceiling((double)totalProductss / pageSizee);

                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPagess;

                var ress = Json(new
                {
                    Html = RenderRazorViewToString("_ListProduct", paginatedProductss),
                    PaginationInfo = new
                    {
                        CurrentPage = page,
                        TotalPages = totalPagess
                    }
                });
                return ress;
            }
            var products = list.Where(p => listid.Contains((Guid)p.FilterId) && p.Product.CategoryId == categoryy.Id)
                               .Select(p => p.Product)
                               .Distinct()
                               .ToList();
            int pageSize = 6;
            var paginatedProducts = products.Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            int totalProducts = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            var res = Json(new
            {
                Html = RenderRazorViewToString("_ListProduct", paginatedProducts),
                PaginationInfo = new
                {
                    CurrentPage = page,
                    TotalPages = totalPages
                }
            });
        
        return res;
        }

        private string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext).Wait();
                return sw.GetStringBuilder().ToString();
            }
        }
    }

}