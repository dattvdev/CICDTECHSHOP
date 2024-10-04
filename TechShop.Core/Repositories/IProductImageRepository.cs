using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public interface IProductImageRepository
    {
        Task<bool> AddProductImage(ProductImage productImage);
        Task<List<ProductImage>> GetListImagesContentByProductId(Guid? productId);
        Task<int> CountListImageContentByProductId(Guid? productId);
        Task<List<ProductImage>> GetListImagesSlideByProductId(Guid? productId);
        Task<bool> DeleteProductImage(ProductImage productImage);
        Task<ProductImage> GetProductImageById(Guid productImageId);
    }
}
