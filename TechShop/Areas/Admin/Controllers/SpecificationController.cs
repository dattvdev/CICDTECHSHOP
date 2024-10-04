using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Text.RegularExpressions;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SpecificationController : Controller
    {
		private readonly TechshopDbContext _context;
		private readonly ISpecificationRepository _specificationRepository;
        private readonly IFilterRepository _filterRepository;
        public SpecificationController(TechshopDbContext context, ISpecificationRepository specificationRepository, IFilterRepository filterRepository)
        {
			_context = context;
			_specificationRepository = specificationRepository;
            _filterRepository = filterRepository;
        }
        public async Task<IActionResult> Index()
        {
            List<Specification> specifications = await _specificationRepository.GetAllSpecifications();
            return View(specifications);
        }

        public async Task<IActionResult> GetSpecificationByCategoryId(string categoryId)
        {
            List<Specification> specifications = await _specificationRepository.GetSpecificationsByCategoryId(Guid.Parse((categoryId)));
            return Json(specifications);
        }

        public async Task<IActionResult> GetFilterByCategoryIdAndSpecificationId(string categoryId, string specificationId)
        {
            List<Filter> filter = await _filterRepository.GetFiltersByCategoryIdAndSpecificationId(Guid.Parse(categoryId), Guid.Parse(specificationId));
            return Json(filter);
        }
		public IActionResult Create()
		{
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
			return PartialView("_CreateModal");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,CategoryId")] Specification specification)
		{

			if (ModelState.IsValid)
			{
				Specification newSpecification = new Specification() { Name = specification.Name, CategoryId = specification.CategoryId };
				_specificationRepository.AddSpecification(newSpecification);
				var specifications = await _specificationRepository.GetAllSpecifications();
				ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name",specification.CategoryId);
			}
			return PartialView("_CreateModal", specification);
		}
		public async Task<IActionResult> Edit(Guid id)
		{
			if (id == Guid.Empty)
			{
				return NotFound();
			}
			var specification = _specificationRepository.Find(id);
			if (specification == null)
			{
				return NotFound();
			}
			ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", specification.CategoryId);
			return PartialView("_EditModal", specification);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid? id, [Bind("Name,CategoryId")] Specification specification)
		{
			var specificationInDB = _specificationRepository.Find(id);
			if (ModelState.IsValid)
			{
				try
				{
					specificationInDB.Name = specification.Name;
					//specificationInDB.CategoryId = specification.CategoryId;
					_specificationRepository.UpdateSpecification(specificationInDB, specification);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (specificationInDB == null)
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				var specifications = await _specificationRepository.GetAllSpecifications();
				return PartialView("_TableView", specifications);
			}
			ViewData["CategoryId"] = new SelectList(_context.Specifications, "Id", "Name", specification.CategoryId);
			return PartialView("_EditModal", specification);
		}
		public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            var category = _specificationRepository.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return PartialView("_DeleteModal", category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id, [Bind("Id")] Specification specification)
        {
            Console.WriteLine(specification.Id + "" + id);
            if (id != specification.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _specificationRepository.DeleteSpecification(specification);
                }
                catch (DbUpdateConcurrencyException)
                {
                }
            }
            var categories = await _specificationRepository.GetAllSpecifications();
            return Json(categories.Select(c => new
            {   
                id = c.Id
            }));
        }
		private bool SpecificationExists(Guid? id)
		{
			return _context.Specifications.Any(e => e.Id == id);
		}
		private string customTrim(string? value)
		{
			if (value == null) return null;
			value = value.Trim();
			value = Regex.Replace(value, @"\s+", " ");
			return value;

		}
		public JsonResult IsSpecificationExist(string name, Guid categoryId)
		{
			string nem = customTrim(name);

			var specificationExist = _context.Specifications.Any(f => f.Name == nem && f.CategoryId.Equals(categoryId));
			return Json(specificationExist);
		}
		[HttpPost]
        public async Task<IActionResult> GetDataSpecification(DataTableParameters parameters)
        {
            await Task.Delay(200); // để cho xịn xịn chút
                                   // Check DataTableParameters 
            if (parameters == null)
            {
                return BadRequest("Invalid parameters");
            }

            var result = await _specificationRepository.GetDataSpecification(parameters);

            return Json(result);
        }
    }
}
