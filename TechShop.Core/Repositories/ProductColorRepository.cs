using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Data;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public class ProductColorRepository : IProductColorRepository
    {
        private readonly TechshopDbContext _context;
        public ProductColorRepository(TechshopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddNewListProductsColor(List<ProductColor> productColors)
        {
            if(productColors is null)
                return false;
            if (productColors.Count == 0)
                return true;

            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var color in productColors)
                {
                    await _context.ProductColors.AddAsync(color);
                }
                await _context.SaveChangesAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
            await transaction.CommitAsync();
            return true;
        }

        public async Task<ProductColor?> GetProductById(string id)
        {
            return await _context.ProductColors.FirstOrDefaultAsync(p => p.Id.ToString().Equals(id));
        }

        public async Task<List<ProductColor>> GetProductColorsByHardwareId(string id)
        {
            return await _context.ProductColors.Where(p => p.ProductHardwareId.ToString().Equals(id)).ToListAsync();
        }

        public async Task<bool> RemoveProductColor(string id)
        {
            var product = await GetProductById(id);
            if (product is null)
                return false;
            try
            {
                _context.ProductColors.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateListProductColor(List<ProductColor> productColors, string productHardwareId)
        {
            var oldProductColors = await GetProductColorsByHardwareId(productHardwareId);
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var oldProdctColorToRemove = (from op in oldProductColors
                                              join np in productColors
                                              on op.Id equals np.Id
                                              into productGroup
                                              from opr in productGroup.DefaultIfEmpty()
                                              select new { op, opr }).Where(c => c.opr is null).Select(c => c.op).ToList();

                foreach (var item in oldProdctColorToRemove)
                {
                    _context.ProductColors.Remove(item);
                }

                foreach (var item in productColors)
                {
                    //var checkExist = oldProductColors.Any(i => i.Id.ToString().Equals(item.Id.ToString(), StringComparison.OrdinalIgnoreCase));
                    if (item.Id is null)
                    {
                        item.CreatedAt = DateTime.Now;
                        await _context.ProductColors.AddAsync(item);
                    }
                    else
                    {
                        var newProduct = oldProductColors.FirstOrDefault(i => i.Id.ToString().Equals(item.Id.ToString(), StringComparison.OrdinalIgnoreCase));
                        newProduct.Quantity = item.Quantity;
                        newProduct.Price = item.Price;
                        newProduct.RGB = item.RGB;
                        newProduct.UpdatedAt = DateTime.Now;

                        _context.ProductColors.Update(newProduct);
                    }
                }
             

                await _context.SaveChangesAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
            await transaction.CommitAsync();
            return true;
        }
    }
}
