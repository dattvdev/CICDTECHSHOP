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
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly TechshopDbContext db;
        public ProductImageRepository(TechshopDbContext context)
        {
            db = context;
        }
        public async Task<bool> AddProductImage(ProductImage productImage)
        {
            db.ProductImages.Add(productImage);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductImage>> GetListImagesContentByProductId(Guid? productId)
        {
            return await db.ProductImages
                 .Where(pi => pi.Type == 2 && pi.ProductId == productId)
                 .OrderBy(pi => pi.CreatedAt)
                 .ToListAsync();
        }

        public async Task<int> CountListImageContentByProductId(Guid? productId)
        {
            return await db.ProductImages
                 .Where(pi => pi.Type == 2 && pi.ProductId == productId)
                 .CountAsync();
        }

        public async Task<List<ProductImage>> GetListImagesSlideByProductId(Guid? productId)
        {
            return await db.ProductImages
              .Where(pi => pi.Type == 1 && pi.ProductId == productId)
              .OrderBy(pi => pi.CreatedAt)
              .ToListAsync();
        }

        public async Task<ProductImage> GetProductImageById(Guid productImageId)
        {
            return await db.ProductImages.FirstOrDefaultAsync(pi => pi.Id == productImageId);
        }

        public async Task<bool> DeleteProductImage(ProductImage productImage)
        {
            if (productImage == null)
            {
                return false;
            }

            db.ProductImages.Remove(productImage);

            await db.SaveChangesAsync();

            return true;
        }

    }
}
