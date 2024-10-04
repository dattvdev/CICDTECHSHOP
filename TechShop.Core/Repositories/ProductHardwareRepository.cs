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
    public class ProductHardwareRepository : IProductHardwareRepository
    {
        private readonly TechshopDbContext db;
        public ProductHardwareRepository(TechshopDbContext context)
        {
            db = context;
        }
        public async Task<bool> ExistProductHardwareName(string productHardwareName, Guid productId)
        {
            return await db.ProductHardwares.AnyAsync(ph => ph.ProductId == productId && ph.Name == productHardwareName);
        }

        public async Task<bool> AddProductHardware(ProductHardware productHardware)
        {
            db.ProductHardwares.Add(productHardware);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProductHardware>> GetListProductHardwareByProductId(Guid? productId)
        {
            return await db.ProductHardwares.Where(ph =>  ph.ProductId == productId).ToListAsync();
        }

        public async Task<ProductHardware?> GetProductHardwareById(string id)
        {
            return await db.ProductHardwares.FirstOrDefaultAsync(i => i.Id.ToString().Equals(id));
        }

        public async Task<bool> UpdateProducthardwareName(string hardwareId, string hardwareName)
        {
            var hardware = await db.ProductHardwares.FirstOrDefaultAsync(i => i.Id.ToString().Equals(hardwareId));
            if (hardware is null)
            {
                return false;
            }
            try
            {
                hardware.Name = hardwareName;
                db.ProductHardwares.Update(hardware);
                await db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteProductHardware(string id)
        {
            var hardware = await db.ProductHardwares.FirstOrDefaultAsync(p => p.Id.ToString().Equals(id));
            try
            {
                db.ProductHardwares.Remove(hardware);
                await db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
