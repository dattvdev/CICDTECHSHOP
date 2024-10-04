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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TechshopDbContext _context;
        public CustomerRepository(TechshopDbContext context) 
        {
            _context = context;
        }
        public async Task<int> Count()
        {
            return await _context.Customers.CountAsync();
        }

        public async Task<User> FindEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(i => i.Email.Contains(email));
        }

        public async Task<object> getCustomerJustByNewProduct()
        {
            var newInvoice = await _context.invoices.Include(ip => ip.InvoiceProducts).Include(i => i.Customer).ThenInclude(c => c.User).OrderByDescending(i => i.CreatedAt).FirstOrDefaultAsync();
            var result = new
            {
                id = newInvoice.Id,
                customerName = newInvoice.Customer.User.FullName,
                createAt = newInvoice.CreatedAt,
                totalPrice = Math.Round((float)newInvoice.InvoiceProducts.Sum(i => i.ProductPrice * i.Quantity),2)
            };

            return result;
        }
        public async Task<Customer> FindCustomerByUserId(Guid? userId)
        {
            return await _context.Customers.FirstOrDefaultAsync(cus => cus.UserID == userId.ToString());
        }
    }
}
