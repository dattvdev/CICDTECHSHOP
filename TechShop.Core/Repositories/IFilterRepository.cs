using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;

namespace TechShop.Core.Repositories
{
    public interface IFilterRepository
    {
        public Filter GetFilterByID(Guid id);
        public List<Filter> GetFilters();
        public void AddFilter(Filter filter);
        public void UpdateFilter(Filter filter);
        public void DeleteFilter(Filter filter);
        public Task<List<Filter>> GetFilterByCategoryId(Guid? categoryId);
        public Task<List<Filter>> GetFiltersByCategoryIdAndSpecificationId(Guid? categoryId, Guid? specificationId);
        public Task<object> GetDataFilters(DataTableParameters parameters);
    }
}
