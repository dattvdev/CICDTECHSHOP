using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task<User> FindEmail(string email);
        Task<int> Count();
        Task<object> getCustomerJustByNewProduct();
        Task<Customer> FindCustomerByUserId(Guid? userId);
    }
}
