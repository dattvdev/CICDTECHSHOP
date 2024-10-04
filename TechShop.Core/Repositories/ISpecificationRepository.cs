using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;

namespace TechShop.Core.Repositories
{
    public interface ISpecificationRepository
    {
        public Specification Find(Guid? specificationId);
		public void AddSpecification(Specification specification);
		public Task<List<Specification>> GetAllSpecifications();
        public Task<List<Specification>> GetSpecificationsByCategoryId(Guid categoryId);
		void UpdateSpecification(Specification specificationInDB, Specification specification);
		public void DeleteSpecification(Specification specification);
        public void DeleteSpecification(Guid? specificationId);
        public Task<object> GetDataSpecification(DataTableParameters parameters);
    }
}
