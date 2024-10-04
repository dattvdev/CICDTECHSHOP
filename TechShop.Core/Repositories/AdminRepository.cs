using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Data;

namespace TechShop.Core.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly TechshopDbContext db;
        public AdminRepository(TechshopDbContext context)
        {
            db = context;
        }
        public bool Exist(string guid)
        {
            return db.Admins.Any(c => c.UserID == guid);
        }
    }
}
