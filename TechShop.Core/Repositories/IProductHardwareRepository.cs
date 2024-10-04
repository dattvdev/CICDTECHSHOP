using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public interface IProductHardwareRepository
    {
        Task<ProductHardware> GetProductHardwareById(string id);
        Task<bool> ExistProductHardwareName(string productHardwareName, Guid productId);
        Task<bool> AddProductHardware(ProductHardware productHardware);
        Task<List<ProductHardware>> GetListProductHardwareByProductId(Guid? productId);
        Task<bool> UpdateProducthardwareName(string hardwareId, string hardwareName);
        Task<bool> DeleteProductHardware(string id);
    }
}
