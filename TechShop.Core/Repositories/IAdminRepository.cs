using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Repositories
{
    public interface IAdminRepository
    {
        public bool Exist(string guid);
    }
}
