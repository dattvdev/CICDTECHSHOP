using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Data;
using TechShop.Core.Models;

namespace TechShop.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TechshopDbContext db;
        public UserRepository(TechshopDbContext context)
        {
            db = context;
        }
        public User Login(string email, string password)
        {
            return db.Users.FirstOrDefault(user => user.Email == email && user.PasswordHash == password);
        }
        public async Task<User> FindUserByUsername(string username)
        {
            return await db.Users.FirstOrDefaultAsync(us => us.UserName == username);
        }
    }
}
