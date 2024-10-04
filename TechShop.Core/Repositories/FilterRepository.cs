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
    public class FilterRepository : IFilterRepository
    {

        private readonly TechshopDbContext db;
        public FilterRepository(TechshopDbContext context)
        {
            db = context;
        }
        void IFilterRepository.AddFilter(Filter filter)
        {
            db.Filters.Add(filter);
            db.SaveChanges();
        }

        void IFilterRepository.DeleteFilter(Filter filter)
        {
            IProductFilterRepository pfr = new ProductFilterRepository(db);
            pfr.DeleteByFilter(filter.Id);
            db.Filters.Remove(filter);
            db.SaveChanges();
        }

        Filter IFilterRepository.GetFilterByID(Guid id)
        {
            return db.Filters.FirstOrDefault(c => c.Id == id);
        }

        List<Filter> IFilterRepository.GetFilters()
        {
            return db.Filters.Include(c => c.Category).ToList();
        }

        void IFilterRepository.UpdateFilter(Filter filter)
        {
            db.Update(filter);
            db.SaveChanges();
        }

        public async Task<List<Filter>> GetFilterByCategoryId(Guid? categoryId)
        {
            return await db.Filters.Where(fl => fl.CategoryId == categoryId).ToListAsync();
        }
        public async Task<List<Filter>> GetFiltersByCategoryIdAndSpecificationId(Guid? categoryId, Guid? specificationId)
        {
            return await db.Filters.Where(fl => fl.CategoryId == categoryId && fl.SpecificationId == specificationId).ToListAsync();
        }
        public async Task<object> GetDataFilters(DataTableParameters parameters)
        {
            if (parameters.Order == null || !parameters.Order.Any())
            {
                parameters.Order = new Order[] { new Order { Column = 0, Dir = "asc" } };
            }

            // Take list comments from database
            var filters = db.Filters.AsQueryable();


            // mapping data
            var query = filters
                .Select(i => new
                {
                    id = i.Id,
                    name = i.Name,
                    catename = i.Category.Name,
                    spename = i.Specification.Name,
                    createAt = i.CreatedAt.HasValue
                        ? $"{i.CreatedAt.Value.Day}/{i.CreatedAt.Value.Month}/{i.CreatedAt.Value.Year}"
                        : "Unknown",
                    createAtDate = i.CreatedAt.HasValue ? i.CreatedAt.Value : (DateTime?)null,
                    updatedAt = i.UpdatedAt.HasValue
                        ? $"{i.UpdatedAt.Value.Day}/{i.UpdatedAt.Value.Month}/{i.UpdatedAt.Value.Year}"
                        : "Unknown",
                    updatedAtDate = i.UpdatedAt.HasValue ? i.UpdatedAt.Value : (DateTime?)null
                });

            // search for DataTableParameters 
            if (!string.IsNullOrEmpty(parameters.Search?.Value))
            {
                query = query.Where(i => i.name.Contains(parameters.Search.Value) || i.catename.Contains(parameters.Search.Value));
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
                        ? query.OrderBy(i => i.catename)
                        : query.OrderByDescending(i => i.catename);
                    break;
                case 3:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.createAtDate)
                        : query.OrderByDescending(i => i.createAtDate);
                    break;
                case 4:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.updatedAtDate)
                        : query.OrderByDescending(i => i.updatedAtDate);
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
    }
}
