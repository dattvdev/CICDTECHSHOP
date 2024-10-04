using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FiltersController : Controller
    {
        private readonly TechshopDbContext _context;
        private readonly IFilterRepository _filterRepository;
        private readonly ISpecificationRepository _specificationRepository;
        public FiltersController(TechshopDbContext context, IFilterRepository filterRepository, ISpecificationRepository specificationRepository)
        {
            _context = context;
            _filterRepository = filterRepository;
            _specificationRepository = specificationRepository;
        }

        // GET: Admin/Filters
        public async Task<IActionResult> Index()
        {
            var filters =  _filterRepository.GetFilters();
            return View(filters);
        }


        // GET: Admin/Filters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filter = await _context.Filters
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (filter == null)
            {
                return NotFound();
            }

            return View(filter);
        }

        // GET: Admin/Filters/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return PartialView("_Create");
        }

        // POST: Admin/Filters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId,SpecificationId")] Filter filter)
        {
            if (ModelState.IsValid)
            {
                filter.CreatedAt = DateTime.Now;
                filter.UpdatedAt = DateTime.Now;
                filter.Name = customTrim(filter.Name);
                _filterRepository.AddFilter(filter);
                var filters = _filterRepository.GetFilters();
                return PartialView("_TableView", filters); // Trả về partial view mới
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", filter.CategoryId);
            return PartialView("_Create", filter);
        }

        // GET: Admin/Filters/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var filter = _filterRepository.GetFilterByID(id);
            if (filter == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", filter.CategoryId);
            return PartialView("_Edit", filter);
        }

        // POST: Admin/Filters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("Id,Name,CategoryId,SpecificationId,CreatedAt")] Filter filter)
        {
            if (id != filter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    filter.UpdatedAt = DateTime.Now;
                    filter.Name = customTrim(filter.Name);
                    _filterRepository.UpdateFilter(filter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilterExists(filter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var filters = _filterRepository.GetFilters();
                return PartialView("_TableView", filters); 
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", filter.CategoryId);
            return PartialView("_Edit", filter);
        }
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var filter = _filterRepository.GetFilterByID(id);
            if (filter == null)
            {
                return NotFound();
            }
            return PartialView("_Delete", filter);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid? id, [Bind("Id")] Filter filter)
        {
            if (id != filter.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _filterRepository.DeleteFilter(filter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilterExists(filter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            var filters = _filterRepository.GetFilters();
            return PartialView("_TableView", filters);
        }

        private bool FilterExists(Guid? id)
        {
            return _context.Filters.Any(e => e.Id == id);
        }
        private string customTrim(string? value)
        {
            if (value == null) return null;
            value = value.Trim();
            value = Regex.Replace(value, @"\s+", " ");
            return value;

        }
        public JsonResult IsFilterExist(string name, Guid categoryId, Guid specificationId)
        {
            string nem = customTrim(name);

            var filterExist = _context.Filters.Any(f => f.Name == nem && f.CategoryId.Equals(categoryId) && f.SpecificationId.Equals(specificationId));
            return Json(filterExist); 
        }


        [HttpPost]
        public async Task<IActionResult> GetDataFilters(DataTableParameters parameters)
        {
            await Task.Delay(100); // để cho xịn xịn chút
            // Check DataTableParameters 
            if (parameters == null)
            {
                return BadRequest("Invalid parameters");
            }

            var result = await _filterRepository.GetDataFilters(parameters);

            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetSpecificationsByCategory(string categoryId)
        {
            List<Specification> specifications = await _specificationRepository.GetSpecificationsByCategoryId(Guid.Parse(categoryId));
            return Json(specifications);
        }

    }
}
