using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Repositories
{
    public interface IProductFilterRepository
    {
        public void DeleteByFilter(Guid? id);
        public Task<List<Guid?>> GetProductFilterIds(Guid? productId);
        public Task<bool> UpdateFilters(Guid? productId, List<Guid> selectedFilterIds);
    }
}
