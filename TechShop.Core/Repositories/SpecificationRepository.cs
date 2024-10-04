using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Data;
using TechShop.Core.Models;
using Microsoft.EntityFrameworkCore;
using TechShop.Core.Models.DataTableModel;
using Order = TechShop.Core.Models.DataTableModel.Order;

namespace TechShop.Core.Repositories
{
    public class SpecificationRepository : ISpecificationRepository
    {
        private readonly TechshopDbContext db;
        public SpecificationRepository(TechshopDbContext context)
        {
            db = context;
        }

        public async Task<List<Specification>> GetAllSpecifications()
        {
            return await db.Specifications.Include(sp => sp.Category).ToListAsync();
        }

        public async Task<List<Specification>> GetSpecificationsByCategoryId(Guid categoryId)
        {
            return await db.Specifications.Where(sp => sp.CategoryId == categoryId).ToListAsync();
        }
		public void AddSpecification(Specification specification)
        {
			db.Specifications.Add(specification);
			db.SaveChanges();
		}

		public void DeleteSpecification(Specification specification)
        {
            Specification item = Find(specification.Id);
            db.Specifications.Remove(item);

            db.SaveChanges();
        }
		public void UpdateSpecification(Specification specificationInDB, Specification specification)
		{
			Specification item = Find(specificationInDB.Id);
            item.CategoryId = specification.CategoryId;
			db.Update(item);
			db.SaveChanges();
		}
		public void DeleteSpecification(Guid? specificationId)
        {
            var item = db.Specifications.Find(specificationId);
            db.Specifications.Remove(item);
            db.SaveChanges();
        }
        public Specification Find(Guid? specificationId)
        {
            return db.Specifications.SingleOrDefault(c => c.Id == specificationId);
        }
        public async Task<object> GetDataSpecification(DataTableParameters parameters)
        {
            if (parameters.Order == null || !parameters.Order.Any())
            {
                parameters.Order = new Order[] { new Order { Column = 0, Dir = "asc" } };
            }
            // Take list comments from database
            var specification = db.Specifications.AsQueryable();

            // mapping data
            var query = specification
                .Select(i => new
                {
                    id = i.Id,
                    name = i.Name,
                    categoryId = i.CategoryId,
                    categoryName = i.Category.Name,
                });

            // search for DataTableParameters 
            if (!string.IsNullOrEmpty(parameters.Search?.Value))
            {
                query = query.Where(i => i.name.Contains(parameters.Search.Value) || i.categoryName.Contains(parameters.Search.Value));
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
                        ? query.OrderBy(i => i.categoryName)
                        : query.OrderByDescending(i => i.categoryName);
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

		public void UpdateSpecification(Specification specification, Guid? categoryId)
		{
			throw new NotImplementedException();
		}
	}
}
