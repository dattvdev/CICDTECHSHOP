using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;

namespace TechShop.Core.Repositories
{
    public interface IBrandRepository
    {
        Brand GetBrandByID(Guid brandId);
        void Update(Brand brand);
        void Delete(Brand brand);
        void Add(Brand brand);
        List<Brand> GetAllBrands();
        int CountBrands();
        public Task<object> GetDataBrands(DataTableParameters parameters);
    }
}
