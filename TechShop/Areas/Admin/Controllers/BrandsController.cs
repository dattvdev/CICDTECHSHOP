using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandsController : Controller
    {
        private readonly TechshopDbContext _context;
        private readonly IBrandRepository _brandRepository;

        public BrandsController(IBrandRepository brand, TechshopDbContext context)
        {
            _brandRepository = brand;
            _context = context;
        }

        // GET: Admin/Brands
        public async Task<IActionResult> Index()
        {
            return View( _brandRepository.GetAllBrands());
        }

        // GET: Admin/Brands/Create
        public IActionResult Create()
        {
            return PartialView("_CreateModal");
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                brand.UrlSlug = Utilities.Utilities.ConvertToSlug(brand.Name);
                brand.CreatedAt = DateTime.Now;
                brand.UpdatedAt = DateTime.Now;
                brand.Name = customTrim(brand.Name);
                _brandRepository.Add(brand);
                var brands = _brandRepository.GetAllBrands();
                return PartialView("_TableView", brands); // Trả về partial view mới
            }
            return PartialView("_CreateModal", brand);
        }

        // GET: Admin/Brands/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var brand = _brandRepository.GetBrandByID(id);
            if (brand == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("Id,Name,Description,CreatedAt,UrlSlug")] Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    brand.UpdatedAt = DateTime.Now;
                    brand.Name = customTrim(brand.Name);
                    brand.UrlSlug = Utilities.Utilities.ConvertToSlug(brand.Name);
                    _brandRepository.Update(brand);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var brands = _brandRepository.GetAllBrands();
                return PartialView("_TableView", brands);
            }
            return PartialView("_EditModal", brand);
        }

        // GET: Admin/Brands/Delete
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var brand = _brandRepository.GetBrandByID(id);
            if (brand == null)
            {
                return NotFound();
            }
            return PartialView("_DeleteModal", brand);

        }

        //POST: Admin/Brands/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id, [Bind("Id")] Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _brandRepository.Delete(brand);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            var brands = _brandRepository.GetAllBrands();
            return PartialView("_TableView", brands);
        }

        private bool BrandExists(Guid? id)
        {
            return _context.Brands.Any(b => b.Id == id);
        }
        private string customTrim(string? value)
        {
            if (value == null) return null;
            value = value.Trim();
            value = Regex.Replace(value, @"\s+", " ");
            return value;

        }
        public JsonResult IsBrandExist(Guid id,string name,string? des)
        {
            string brandName = customTrim(name);

            var brandExist = _context.Brands.Any(b => b.Name == brandName);
            if (brandExist)
            {
                var brand = _context.Brands.FirstOrDefault(b => b.Name == brandName);
                if ((des == null && brand.Description==null) || (des!=null && brand.Description!= null && des.Equals(brand.Description)))
                {
                    return Json(brandExist);
                }
                if (id.Equals(brand.Id)) return Json(false);      
            }
            return Json(brandExist); // Trả về true nếu không tồn tại, false nếu tồn tại
        }

        [HttpPost]
        public async Task<IActionResult> GetDataBrands(DataTableParameters parameters)
        {
            await Task.Delay(200); // để cho xịn xịn chút
            // Check DataTableParameters 
            if (parameters == null)
            {
                return BadRequest("Invalid parameters");
            }

            var result = await _brandRepository.GetDataBrands(parameters);

            return Json(result);
        }
    }
}
