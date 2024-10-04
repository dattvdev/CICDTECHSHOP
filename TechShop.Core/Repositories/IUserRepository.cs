using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public interface IUserRepository
    {
        public User Login(string username, string password);
        public Task<User> FindUserByUsername(string username);
    }
}
