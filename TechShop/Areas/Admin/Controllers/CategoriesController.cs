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
using static NuGet.Packaging.PackagingConstants;

namespace TechShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _repository;
		private readonly TechshopDbContext _context;

		public CategoriesController(ICategoryRepository category, TechshopDbContext context)
        {
            _repository = category;
			_context = context;

		}

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            return View( _repository.GetAllCategories());
        }

		[HttpGet]
		public async Task<IActionResult> GetCategory(Guid id)
		{
			var category =  _repository.Find(id); // Fetch the category from the database
			if (category == null)
			{
				return NotFound();
			}

			// Return the category data as JSON
			return Json(new { name = category.Name, description = category.Description });
		}

		// GET: Admin/Categories/Create
		public IActionResult Create()
		{
			
			return PartialView("_CreateModal");
		}
		// POST: Admin/Categories/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Category category)
        {
            
			if (ModelState.IsValid)
            {
                Category newCategory = new Category() { Name= category.Name, Description= category.Description, UrlSlug=Utilities.Utilities.ConvertToSlug(category.Name),CreatedAt=DateTime.Now };
                _repository.AddCategory(newCategory);
                var categorys = _repository.GetAllCategories();
                return Json(categorys);
            }
            return Json(category);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var category = _repository.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return PartialView("_EditModal", category);
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid? id, [Bind("Name,Description")] Category category)
		{
            var categoryInDB =  _repository.Find(id);
			if (ModelState.IsValid)
			{
				try
				{
                    categoryInDB.Name = category.Name;
                    categoryInDB.Description =category.Description;
                    categoryInDB.UrlSlug = Utilities.Utilities.ConvertToSlug(category.Name);
					_repository.UpdateCategory(categoryInDB);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (categoryInDB==null)
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
                var categories = _repository.GetAllCategories();
                return PartialView("_TableCategoryView", categories);
            }
			return PartialView("_EditModal", category);
		}

        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var category = _repository.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return PartialView("_DeleteModal", category);

        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid? id, [Bind("Id")] Category category)
		{
            Console.WriteLine(category.Id+""+id);
            if (id != category.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.DeleteCategory(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                }
            }
            var categories = _repository.GetAllCategories();
            return Json(categories);
        }
		private string customTrim(string value)
		{
			value = value.Trim();
			value = Regex.Replace(value, @"\s+", " ");
			return value;

		}

		[HttpPost]
		public async Task<IActionResult> GetDataCategory(DataTableParameters parameters)
		{
			await Task.Delay(200); // để cho xịn xịn chút
								   // Check DataTableParameters 
			if (parameters == null)
			{
				return BadRequest("Invalid parameters");
			}

			var result = await _repository.GetDataCategory(parameters);

			return Json(result);
		}
		public JsonResult IsCategoryExist(string name, Guid id)
		{
			string nem = customTrim(name);

			var categoryExist = _context.Categories.Any(f => f.Name == nem && f.Id.Equals(id));
			return Json(categoryExist); // Trả về true nếu không tồn tại, false nếu tồn tại
		}

		// GET: Admin/Categories/Edit/5
		/*public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("Id,Name,UrlSlug")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

		/*private bool CategoryExists(Guid? id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }*/
	}
}
