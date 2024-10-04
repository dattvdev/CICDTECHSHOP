using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using Order = TechShop.Core.Models.DataTableModel.Order;

namespace TechShop.Core.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TechshopDbContext db;

        public CategoryRepository(TechshopDbContext context)
        {
            db = context;
        }

        public void AddCategory(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            Category item = Find(category.Id);

			// Fetch all filters related to the category
			var filters = db.Filters.Include(f => f.ProductFilters)
									.Where(f => f.CategoryId == category.Id).ToList();

			foreach (var filter in filters)
			{
				// Remove all associated ProductFilters
				db.ProductFilters.RemoveRange(filter.ProductFilters);

				// Now remove the filter itself
				db.Filters.Remove(filter);
			}

			// Finally, remove the category
			db.Categories.Remove(item);

			db.SaveChanges();
		}

        public void DeleteCategory(Guid? categoryId)
        {
            var item = db.Categories.Find(categoryId);
            db.Categories.Remove(item);
            db.SaveChanges();
        }

        public Category Find(Guid? categoryId)
        {
            return db.Categories.SingleOrDefault(c =>c.Id==categoryId);
        }

        public List<Category> GetAllCategories()
        {
            return db.Categories.OrderBy(c => c.CreatedAt).ToList();
        }

		public async Task<object> GetDataCategory(DataTableParameters parameters)
		{
			if (parameters.Order == null || !parameters.Order.Any())
			{
				parameters.Order = new Order[] { new Order { Column = 0, Dir = "asc" } };
			}
			// Take list comments from database
			var category = db.Categories.AsQueryable();

			// mapping data
			var query = category
				.Select(i => new
				{
					id = i.Id,
					name = i.Name,
					description = i.Description,
					urlSlug= i.UrlSlug,
					createAt = i.CreatedAt.HasValue
						? $"{i.CreatedAt.Value.Day}/{i.CreatedAt.Value.Month}/{i.CreatedAt.Value.Year}"
						: "Unknown",
					createAtDate = i.CreatedAt.HasValue ? i.CreatedAt.Value : (DateTime?)null
				});

			// search for DataTableParameters 
			if (!string.IsNullOrEmpty(parameters.Search?.Value))
			{
				query = query.Where(i => i.name.Contains(parameters.Search.Value) || i.description.Contains(parameters.Search.Value));
			}

			// sort for DataTableParameters
			var sortColumn = parameters.Order[0].Column;
			var sortDirection = parameters.Order[0].Dir;
			switch (sortColumn)
			{
				case 1:
					query = sortDirection == "asc"
						? query.OrderBy(i => i.name)
						: query.OrderByDescending(i => i.name);
					break;
				case 2:
					query = sortDirection == "asc"
						? query.OrderBy(i => i.description)
						: query.OrderByDescending(i => i.description);
					break;
				case 3:
					query = sortDirection == "asc"
						? query.OrderBy(i => i.urlSlug)
						: query.OrderByDescending(i => i.urlSlug);
					break;
				case 4:
					query = sortDirection == "asc"
						? query.OrderBy(i => i.createAtDate)
						: query.OrderByDescending(i => i.createAtDate);
					break;
				default:
					query = query.OrderBy(i => i.name);
					break;
			}

			// Count total records
			var totalRecords = await query.CountAsync();
			//Paging
			var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();

			// end
			var result = new
			{
				draw = parameters.Draw,
				recordsTotal = totalRecords,
				recordsFiltered = totalRecords,
				data = data,
			};

			return result;
		}

		public void UpdateCategory(Category category)
        {
            db.SaveChanges();
        }

	}
}
