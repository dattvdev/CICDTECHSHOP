using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;

namespace TechShop.Core.Repositories
{
    public interface IProductRepository
    {
        public List<Product> GetProducts();
        public List<Product> GetProductsByCategoryBrandId(Guid categoryId, Guid brandId);
        public List<Product> GetProductsByCategoryId(Guid categoryId);
        public List<Product> GetProductsByBrandId(Guid brandId);
        public Task<Product> GetProductById(Guid id);
        public Task<Product> GetProductByUrlSlug(string urlSlug);
        public int CountProducts();
        public List<ProductWithMinPrice> GetProductsWithPrice();
        public List<ProductWithMinPrice> GetProductsByCategory(string categoryName);
        public Product NewProduct(string productName, string urlSlug, Guid brandId, Guid categoryId);
        public bool IsExistProductName(string productName, string urlSlug, Guid? productId);
        public Task<bool> UpdateProduct(Product product);
        public void DeleteProduct(Product product);
        public Task<object> GetDataProduct(DataTableParameters parameters);
    }
}
