using Microsoft.EntityFrameworkCore;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using Order = TechShop.Core.Models.DataTableModel.Order;

namespace TechShop.Core.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TechshopDbContext db;
        public ProductRepository(TechshopDbContext context)
        {
            db = context;
        }
        public async Task<Product> GetProductById(Guid id)
        {
            return await db.Products
                .Include(product => product.Category)
                .Include(product => product.Brand)
                .FirstOrDefaultAsync(product => product.Id == id);
        }
        public async Task<Product> GetProductByUrlSlug(string urlSlug)
        {
            return await db.Products
                .Include(product => product.Category)
                .Include(product => product.Brand)
                .Include(product => product.Images)
				.Include(product => product.ProductHardwares)
                .ThenInclude(hardware => hardware.ProductColors)
				.FirstOrDefaultAsync(product => product.UrlSlug == urlSlug);
        }
        public List<Product> GetProductsByCategoryBrandId(Guid categoryId, Guid brandId) {
            return db.Products
            .Include(product => product.Category)
            .Include(product => product.Brand)
            .Where(product => product.CategoryId == categoryId && product.BrandId == brandId)
            .ToList();
        }
        public List<Product> GetProductsByCategoryId(Guid categoryId)
        {
            return db.Products
            .Include(product => product.Category)
            .Include(product => product.Brand)
            .Where(product => product.CategoryId == categoryId)
            .ToList();
        }
        public List<Product> GetProductsByBrandId(Guid brandId)
        {
            return db.Products
            .Include(product => product.Category)
            .Include(product => product.Brand)
            .Where(product => product.BrandId == brandId)
            .ToList();
        }
        public List<Product> GetProducts()
        {
            return db.Products.Include(product => product.Category).Include(product => product.Brand).ToList();
        }
		public List<ProductWithMinPrice> GetProductsWithPrice()
		{
			var products = from product in db.Products
						   join productHardware in db.ProductHardwares
						   on product.Id equals productHardware.ProductId
						   join productCategory in db.Categories
						   on product.CategoryId equals productCategory.Id
						   join productColor in db.ProductColors
						   on productHardware.Id equals productColor.ProductHardwareId
						   group productColor by new
						   {
							   product.Name,
							   product.Discount,
							   product.Image,
							   product.UrlSlug,
							   CateName = productCategory.Name
						   } into productGroup
						   select new ProductWithMinPrice
						   {
							   Name = productGroup.Key.Name,
							   Discount = productGroup.Key.Discount ?? 0, 
							   CateName = productGroup.Key.CateName,
							   Image = productGroup.Key.Image,
							   UrlSlug = productGroup.Key.UrlSlug,
							   MinPrice = productGroup.Min(pc => pc.Price)
						   };
			return products.ToList();
		}
		public List<ProductWithMinPrice> GetProductsByCategory(string categoryName)
		{
            var cate = db.Categories.FirstOrDefault(c => c.Name == categoryName);

            var products = db.Products
                .Include(product => product.Category)
                .Include(product => product.ProductHardwares)
                    .ThenInclude(hardware => hardware.ProductColors)
                .Where(p => p.CategoryId.ToString().Equals(cate.Id.ToString()))
               .Select(product => new ProductWithMinPrice
               {
                   Name = product.Name,
                   Discount = (int)product.Discount == null ? 0 : (int)product.Discount,
                   CateName = product.Category.Name,
                   Image = product.Image,
                   UrlSlug = product.UrlSlug,
                   MinPrice = (double)product.ProductHardwares.SelectMany(c => c.ProductColors).Min(pc => pc.Price) == null ? 0 : (double)product.ProductHardwares.SelectMany(c => c.ProductColors).Min(pc => pc.Price)
               }).ToList();

            return products;
        }
		public int CountProducts()
        {
            return db.Products.Count();
        }

		public Product NewProduct(string productName, string urlSlug, Guid brandId, Guid categoryId)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
				BrandId = brandId,
				CategoryId = categoryId,
				CreatedAt = DateTime.Now,
				Discount = 0,
				Description = "",
				Image = "",
				Name = productName,
                UpdatedAt = DateTime.Now,
				UrlSlug = urlSlug,
			};
            db.Products.Add(product);
            db.SaveChanges();
            return product;
        }

        public bool IsExistProductName(string productName, string urlSlug, Guid? productId) {
            if(productId is null)
            {
                return db.Products.Any(product => product.Name == productName || product.UrlSlug == urlSlug);
            } else
            {
                var product = db.Products.FirstOrDefault(product => product.Id == productId);
                if (product.Name == productName && product.UrlSlug == urlSlug)
                {
                    return false;
                }
                else
                {
                    return db.Products.Any(product => product.Name == productName || product.UrlSlug == urlSlug);
                }
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            product.UpdatedAt = DateTime.Now;
            db.Products.Update(product);
            await db.SaveChangesAsync();
            return true;
        }

        public void DeleteProduct(Product product)
        {
            var listImg = db.ProductImages.Where(c => c.ProductId == product.Id).ToList();
            var listInv = db.InvoiceProducts.Where(c => c.ProductColor.ProductHardware.ProductId == product.Id).ToList();
            var listFil = db.ProductFilters.Where(c => c.ProductId == product.Id).ToList();
            var listOrd = db.Orders.Where(c => c.ProductColor.ProductHardware.Product.Id == product.Id).ToList();
            var listCmt = db.Comments.Where(c => c.ProductId == product.Id).ToList();
            var listHwe = db.ProductHardwares.Include(f => f.ProductColors).Where(c => c.ProductId == product.Id).ToList();
            db.ProductImages.RemoveRange(listImg);
            db.InvoiceProducts.RemoveRange(listInv);
            db.ProductFilters.RemoveRange(listFil);
            db.Orders.RemoveRange(listOrd);
            db.Comments.RemoveRange(listCmt);
            foreach (var item in listHwe)
            {
                db.ProductColors.RemoveRange(item.ProductColors);
                db.ProductHardwares.Remove(item);
            }
            db.Products.Remove(product);
            var count = db.SaveChanges();
        }

        public async Task<object> GetDataProduct(DataTableParameters parameters)
        {
            if (parameters.Order == null || !parameters.Order.Any())
            {
                parameters.Order = new Order[] { new Order { Column = 0, Dir = "asc" } };
            }
            // Take list comments from database
            var product = db.Products.Include(c=>c.Category).Include(b=>b.Brand).AsQueryable();

            // mapping data
            var query = product
                .Select(i => new
                {
                    id = i.Id,
                    name = i.Name,
                    categoryName = i.Category.Name,
                    brandName= i.Brand.Name,
                    urlSlug = i.UrlSlug,
                    createdAt = i.CreatedAt,
                    updatedAt = i.UpdatedAt
                });

            // search for DataTableParameters 
            if (!string.IsNullOrEmpty(parameters.Search?.Value))
            {
                query = query.Where(i => i.name.Contains(parameters.Search.Value) || i.categoryName.Contains(parameters.Search.Value) || i.brandName.Contains(parameters.Search.Value));
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
                        ? query.OrderBy(i => i.categoryName)
                        : query.OrderByDescending(i => i.categoryName);
                    break;
                case 3:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.urlSlug)
                        : query.OrderByDescending(i => i.urlSlug);
                    break;
                case 4:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.createdAt)
                        : query.OrderByDescending(i => i.createdAt);
                    break;
                case 5:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.updatedAt)
                        : query.OrderByDescending(i => i.updatedAt);
                    break;
                case 6:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.brandName)
                        : query.OrderByDescending(i => i.brandName);
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
