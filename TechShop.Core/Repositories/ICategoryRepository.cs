using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;

namespace TechShop.Core.Repositories
{
    public interface ICategoryRepository
    {
        Category Find(Guid? categoryId);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        void DeleteCategory(Guid? categoryId);
        List<Category> GetAllCategories();

		public Task<object> GetDataCategory(DataTableParameters parameters);

	}
}
