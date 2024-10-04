using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Data;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public class ProductFilterRepository : IProductFilterRepository
    {

        private readonly TechshopDbContext db;
        public ProductFilterRepository(TechshopDbContext context)
        {
            db = context;
        }
        public void DeleteByFilter(Guid? id)
        {
            var list = db.ProductFilters.Where(f => f.FilterId == id).ToList();
            db.ProductFilters.RemoveRange(list);
            db.SaveChanges();
        }

        public async Task<List<Guid?>> GetProductFilterIds(Guid? productId)
        {
            return await db.ProductFilters
                .Where(pf => pf.ProductId == productId)
                .Select(pf => pf.FilterId)
                .ToListAsync();
        }

            public async Task<bool> UpdateFilters(Guid? productId, List<Guid> selectedFilterIds)
            {
                var product = await db.Products.SingleOrDefaultAsync(p => p.Id == productId);

                if(product == null)
                {
                    return false;
                }

                await db.ProductFilters.Where(pf => pf.ProductId == productId).ExecuteDeleteAsync();

                await db.SaveChangesAsync();

                foreach (var filter in selectedFilterIds)
                {
                    ProductFilter productFilter = new ProductFilter();
                    productFilter.FilterId = filter;
                    productFilter.ProductId = productId;
                    await db.ProductFilters.AddAsync(productFilter);
                    await db.SaveChangesAsync();
                }

                return true;
            }
    }
}
