using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public interface IProductColorRepository
    {
        public Task<ProductColor> GetProductById(string id);
        public Task<List<ProductColor>> GetProductColorsByHardwareId(string id);
        public Task<bool> AddNewListProductsColor(List<ProductColor> productColors);
        public Task<bool> RemoveProductColor(string id);
        public Task<bool> UpdateListProductColor(List<ProductColor> productColors, string hardwareProductId);
    }
}
