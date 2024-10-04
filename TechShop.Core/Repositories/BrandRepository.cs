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
    public class BrandRepository : IBrandRepository
    {
        private readonly TechshopDbContext db;

        public BrandRepository(TechshopDbContext context)
        {
            db = context;
        }

        public void Add(Brand brand)
        {
            db.Add(brand);
            db.SaveChanges();
        }

        public int CountBrands()
        {
            return db.Brands.Count();
        }

        public void Delete(Brand brand)
        {
           IProductRepository pdb = new ProductRepository(db);
            var list = db.Products.Where(p => p.BrandId == brand.Id).ToList();
            foreach ( var product in list )
            {
                pdb.DeleteProduct(product);
            }
            db.Brands.Remove(brand);
            var i = db.SaveChanges();
        }

        public List<Brand> GetAllBrands()
        {
            return db.Brands.ToList();
        }

        public Brand GetBrandByID(Guid brandId)
        {
            return db.Brands.FirstOrDefault(b => b.Id == brandId);
        }

        public async Task<object> GetDataBrands(DataTableParameters parameters)
        {
            // Check order
            if (parameters.Order == null || !parameters.Order.Any())
            {
                parameters.Order = new Order[] { new Order { Column = 0, Dir = "asc" } };
            }

            // take brand list form database
            var brands = db.Brands.AsQueryable();

            // mapping data
            var query = brands
                .Select(i => new
                {
                    id = i.Id,
                    name = i.Name,
                    urlSlug = i.UrlSlug,
                    createAt = i.CreatedAt.HasValue
                        ? $"{i.CreatedAt.Value.Day}/{i.CreatedAt.Value.Month}/{i.CreatedAt.Value.Year}"
                        : "Unknown",
                    updateAt = i.UpdatedAt.HasValue
                        ? $"{i.UpdatedAt.Value.Day}/{i.UpdatedAt.Value.Month}/{i.UpdatedAt.Value.Year}"
                        : "Unknown",
                    description = i.Description
                });

            // search for DataTableParameters, search with field Name
            if (!string.IsNullOrEmpty(parameters.Search?.Value))
            {
                query = query.Where(i => i.name.Contains(parameters.Search.Value));
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
                        ? query.OrderBy(i => i.urlSlug)
                        : query.OrderByDescending(i => i.urlSlug);
                    break;
                case 3:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.description)
                        : query.OrderByDescending(i => i.description);
                    break;
                case 4:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.createAt)
                        : query.OrderByDescending(i => i.createAt);
                    break;
                case 5:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.updateAt)
                        : query.OrderByDescending(i => i.updateAt);
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

        public void Update(Brand brand)
        { 
            db.Update(brand);
            db.SaveChanges();
        }
    }
}
