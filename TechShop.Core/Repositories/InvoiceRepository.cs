using Microsoft.EntityFrameworkCore;
using TechShop.Core.Data;
using TechShop.Core.Models.DataTableModel;
using TechShop.Core.Models;
using Order = TechShop.Core.Models.DataTableModel.Order;
using TechShop.Core.Models.InvoiceParamesters;
using System.Linq.Expressions;
using System.Net.NetworkInformation;

namespace TechShop.Core.Repositories
{
	public class InvoiceRepository : IInvoiceRepository
    {
        private readonly TechshopDbContext _context;
        public InvoiceRepository(TechshopDbContext context) 
        {
            _context = context;
        }

        public async Task<object> CancelInvoice(string id)
        {
            var invoice = await _context.invoices.FirstOrDefaultAsync(i => i.Id.ToString().Equals(id));
            if (invoice is null) return new { isSuccess = false, message = "Not Found"};
            if(invoice.Status == 0)
                return new { isSuccess = false, message = "This invoice already cancel" };
            
            if(invoice.Status == 2 || invoice.Status == 3)
            {
                var mess = invoice.Status == 2 ? "delivering" : "Complete";
                return new { isSuccess = false, message = $"This invoice can't cancel because it is {mess}" };
            }

            invoice.Status = 0;
            invoice.UpdatedAt = DateTime.Now;
            _context.invoices.Update(invoice);
            await _context.SaveChangesAsync();

            return new { isSuccess = true, message = $"Cancel invoice success" };
        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            var invoices = await _context.invoices.Include(i => i.InvoiceProducts).ThenInclude(p=>p.ProductColor).ThenInclude(p => p.ProductHardware).ThenInclude(p => p.Product).Include(u => u.Customer).ThenInclude(u => u.User).ToListAsync();
            return invoices;
        }

        public async Task<List<Invoice>> GetAllInvoicesByMonth(DateTime dateTime)
        {
            var invoices = await _context.invoices
                    .Where(i => i.CreatedAt.HasValue && i.CreatedAt.Value.Month == dateTime.Month && i.CreatedAt.Value.Year == dateTime.Year)
                    .Include(i => i.InvoiceProducts)
                        .ThenInclude(p => p.ProductColor)
                        .ThenInclude(p => p.ProductHardware)
                        .ThenInclude(p => p.Product)
                    .Include(u => u.Customer)
                        .ThenInclude(u => u.User).ToListAsync();
            return invoices;
        }

        public async Task<object?> GetDAUserShoppingInMonth(string userId, string invoiceId)
        {
            var user = await _context.Customers.Include(u => u.User).FirstOrDefaultAsync(c => c.ID.ToString().Equals(userId));
            var invoice = await _context.invoices.FirstOrDefaultAsync(i => i.Id.ToString().Equals(invoiceId));

            if (user == null || invoice == null)
            {
                return null;
            }

            var dbSetLabel = new List<string>
            {
                $"Sales in month of user: {user.User.FullName}"
            };

            var currentDateThisInvoice = invoice.CreatedAt;
            var currentMonth = currentDateThisInvoice.Value.Month;
            var currentYear = currentDateThisInvoice.Value.Year;

            var dailyProductQuantities = await _context.invoices
                .Where(i => i.CustomerId.ToString().Equals(userId) &&
                            i.CreatedAt.Value.Year == currentYear &&
                            i.CreatedAt.Value.Month == currentMonth)
                .GroupBy(i => i.CreatedAt.Value.Day)
                .Select(i => new
                {
                    lable = $"{i.Key}/{currentMonth}/{currentYear}",
                    data0 = i.SelectMany(i => i.InvoiceProducts).Sum(i => i.Quantity)
                })
                .ToListAsync();

            var result = new
            {
                dbSetLable = dbSetLabel,
                result = dailyProductQuantities
            };

            return result;
        }

        public async Task<Invoice?> GetDetailInvoice(string id)
        {
            var invoice = await _context.invoices.Include(p => p.InvoiceProducts).ThenInclude(p => p.ProductColor).ThenInclude(p => p.ProductHardware).ThenInclude(p => p.Product).Include(u => u.Customer).ThenInclude(u => u.User).FirstOrDefaultAsync(i => i.Id.ToString().Equals(id));
     
            return invoice == null ? null : invoice;
        }

        public async Task<object?> GetInvoiceDetailsAsync(DataTableParameters parameters, int? status, DateTime? startDate, DateTime? endDate)
        {

            if (parameters == null)
            {
                return null;
            }

            if (parameters.Order == null || !parameters.Order.Any())
            {
                parameters.Order = new Order[] { new Order { Column = 0, Dir = "asc" } };
            }

            var invoices = _context.invoices.Include(i => i.Customer).ThenInclude(u => u.User).AsQueryable();

            if (status is not null)
            {
                invoices = invoices.Where(i => i.Status == status);
            }

            if (startDate is not null && endDate is null)
            {
                invoices = invoices.Where(i => i.CreatedAt >= startDate && i.CreatedAt <= DateTime.Now);
            }
            else if (startDate is null && endDate is not null)
            {
                invoices = invoices.Where(i => i.CreatedAt <= endDate);
            }
            else if (startDate is not null && endDate is not null)
            {
                invoices = invoices.Where(i => i.CreatedAt >= startDate && i.CreatedAt <= endDate);
            }

            var query = invoices
                .Select(i => new
                {
                    id = i.Id,
                    customerName = i.Customer.User.FullName,
                    createAt = i.CreatedAt.HasValue
                        ? $"{i.CreatedAt.Value.Day}/{i.CreatedAt.Value.Month}/{i.CreatedAt.Value.Year}"
                        : "Unknown",
                    status = i.Status == 1 ? "Prepare" : i.Status == 2 ? "Delivering" : i.Status == 3 ? "Complete" : "Cancel",
                    rawCreateAt = i.CreatedAt,
                    rawStatus = i.Status
                });

            if (!string.IsNullOrEmpty(parameters.Search?.Value))
            {
                query = query.Where(i => i.customerName.Contains(parameters.Search.Value));
            }

            var sortColumn = parameters.Order[0].Column;
            var sortDirection = parameters.Order[0].Dir;

            switch (sortColumn)
            {
                case 1:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.customerName)
                        : query.OrderByDescending(i => i.customerName);
                    break;
                case 2:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.rawCreateAt)
                        : query.OrderByDescending(i => i.rawCreateAt);
                    break;
                case 3:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.rawStatus)
                        : query.OrderByDescending(i => i.rawStatus);
                    break;
                default:
                    query = query.OrderBy(i => i.customerName);
                    break;
            }

            var totalRecords = await query.CountAsync();
            var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();

            var result = new
            {
                draw = parameters.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = data,
            };

            return result;
        }

        public async Task<object> GetInvoiceDetailsAsync_Client(DataTableParameters parameters, string userId)
        {
            if (parameters == null)
            {
                return null;
            }

            if (parameters.Order == null || !parameters.Order.Any())
            {
                parameters.Order = new Order[] { new Order { Column = 0, Dir = "asc" } };
            }

            var invoices = _context.invoices.Include(i => i.Customer).ThenInclude(u => u.User).Where(i => i.Customer.UserID.ToString().Equals(userId)).AsQueryable();

            var query = invoices
                .Select(i => new
                {
                    id = i.Id,
                    customerName = i.Customer.User.FullName,
                    createAt = i.CreatedAt.HasValue
                        ? $"{i.CreatedAt.Value.Day}/{i.CreatedAt.Value.Month}/{i.CreatedAt.Value.Year}"
                        : "Unknown",
                    status = i.Status == 1 ? "Prepare" : i.Status == 2 ? "Delivering" : i.Status == 3 ? "Complete" : "Cancel",
                    rawCreateAt = i.CreatedAt,
                    rawStatus = i.Status
                });

            if (!string.IsNullOrEmpty(parameters.Search?.Value))
            {
                query = query.Where(i => i.customerName.Contains(parameters.Search.Value));
            }

            var sortColumn = parameters.Order[0].Column;
            var sortDirection = parameters.Order[0].Dir;

            switch (sortColumn)
            {
                case 1:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.customerName)
                        : query.OrderByDescending(i => i.customerName);
                    break;
                case 2:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.rawCreateAt)
                        : query.OrderByDescending(i => i.rawCreateAt);
                    break;
                case 3:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.rawStatus)
                        : query.OrderByDescending(i => i.rawStatus);
                    break;
                default:
                    query = query.OrderBy(i => i.customerName);
                    break;
            }

            var totalRecords = await query.CountAsync();
            var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();

            var result = new
            {
                draw = parameters.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = data,
            };

            return result;
        }

		public async Task<object> GetInvoiceForCollaborator(DataTableParameters parameters, string userId)
		{
			if (parameters == null)
			{
				return null;
			}

            if (parameters.Order == null || !parameters.Order.Any())
            {
                parameters.Order = new Order[] { new Order { Column = 0, Dir = "asc" } };
            }

            var invoices = _context.invoices.Include(i => i.Customer).ThenInclude(u => u.User).AsQueryable();

            invoices = invoices.Where(c => c.CollaboratorId.ToString().Equals(userId) || c.CollaboratorId == null);

			var query = invoices
				.Select(i => new
				{
					id = i.Id,
					customerName = i.Customer.User.FullName,
					createAt = i.CreatedAt.HasValue
						? $"{i.CreatedAt.Value.Day}/{i.CreatedAt.Value.Month}/{i.CreatedAt.Value.Year}"
						: "Unknown",
					status = i.Status == 1 ? "Prepare" : i.Status == 2 ? "Delivering" : i.Status == 3 ? "Complete" : "Cancel",
					rawCreateAt = i.CreatedAt,
					rawStatus = i.Status,
					whoTakeCare = i.CollaboratorId == null ? false : true,
                    collaboratorId = i.CollaboratorId
                });

			if (!string.IsNullOrEmpty(parameters.Search?.Value))
			{
				query = query.Where(i => i.customerName.Contains(parameters.Search.Value));
			}

			if (parameters.Order != null || parameters.Order.Any())
			{

				var sortColumn = parameters.Order[0].Column;
				var sortDirection = parameters.Order[0].Dir;

				switch (sortColumn)
				{
					case 1:
						query = sortDirection == "asc"
							? query.OrderBy(i => i.customerName)
							: query.OrderByDescending(i => i.customerName);
						break;
					case 2:
						query = sortDirection == "asc"
							? query.OrderBy(i => i.rawCreateAt)
							: query.OrderByDescending(i => i.rawCreateAt);
						break;
					case 3:
						query = sortDirection == "asc"
							? query.OrderBy(i => i.rawStatus)
							: query.OrderByDescending(i => i.rawStatus);
						break;
					default:
                        query = query.OrderByDescending(c => c.rawStatus == 2)
                                    .ThenByDescending(c => c.rawStatus == 1 && c.whoTakeCare == true)
                                    .ThenByDescending(c => c.rawStatus == 1)
                                    .ThenByDescending(c => c.rawCreateAt);
                        break;
				}
			}

			var totalRecords = await query.CountAsync();
			var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();

			var result = new
			{
				draw = parameters.Draw,
				recordsTotal = totalRecords,
				recordsFiltered = totalRecords,
				data = data,
			};

			return result;
		}

		public async Task<List<Invoice>> GetInvoiceWithOptions(InvoiceParamesterOption invoiceParamester)
        {
            var invoices = _context.invoices.Include(i => i.InvoiceProducts).ThenInclude(p => p.ProductColor).ThenInclude(p => p.ProductHardware).ThenInclude(p => p.Product).Include(u => u.Customer).ThenInclude(u => u.User).AsQueryable();
            invoices = invoices.OrderBy(i => i.CreatedAt);
            if (invoiceParamester is not null)
            {

                if (invoiceParamester.Search is not null)
                {
                    invoices = invoices.Where(i => i.Customer.User.FullName.Contains(invoiceParamester.Search));
                }

                if (invoiceParamester.Status is not null)
                {
                    invoices = invoices.Where(i => i.Status == invoiceParamester.Status);
                }


                if (invoiceParamester.StartDate is not null && invoiceParamester.EndDate is null)
                {
                    invoices = invoices.Where(i => i.CreatedAt >= invoiceParamester.StartDate && i.CreatedAt <= DateTime.Now);
                }
                else if (invoiceParamester.StartDate is null && invoiceParamester.EndDate is not null)
                {
                    invoices = invoices.Where(i => i.CreatedAt <= invoiceParamester.EndDate);
                }
                else if (invoiceParamester.StartDate is not null && invoiceParamester.EndDate is not null)
                {
                    invoices = invoices.Where(i => i.CreatedAt >= invoiceParamester.StartDate && i.CreatedAt <= invoiceParamester.EndDate);
                }
            }

            return await invoices.ToListAsync();
        }

        public async Task<object?> GetProductInInvoice(string id)
        {
            var results = await _context.invoices
                 .Where(o => o.Id.ToString() == id)
                 .SelectMany(i => i.InvoiceProducts)
                 .Select(p => new
                 {
                     id = p.ProductColor.ProductHardware.ProductId,
                     productName = p.ProductColor.ProductHardware.Product.Name,
                     quanity = p.Quantity,
                     ProductPrice = p.ProductPrice
                 })
                 .ToListAsync();

            if (results == null || !results.Any())
            {
                return null;
            }

            return results;
        }

        public async Task<bool> UpdateInvoiceStatus(string invoiceId, string collarId)
        {
            var invoice = await GetDetailInvoice(invoiceId);
            if (invoice is null)
                return false;

            if (invoice.CollaboratorId is null)
                return false;

            if (!invoice.CollaboratorId.ToString().Equals(collarId))
            {
                return false;
            }

            int status = invoice.Status.HasValue ? invoice.Status.Value : 0;
            if(status == 0 || status == 3)
            {
                return false;
            }
            status += 1;
            invoice.Status = status;
            try
            {
                _context.invoices.Update(invoice);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateInvoiceWhoTakeCare(string invoiceId, string collarId)
        {
            var invoice = await GetDetailInvoice(invoiceId);
            if (invoice is null)
                return false;
            if (invoice.CollaboratorId is not null)
                return false;
            invoice.CollaboratorId = new Guid(collarId);
            var beginTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                
                _context.invoices.Update(invoice);
                await _context.SaveChangesAsync();
            }
            catch
            {
                await beginTransaction.RollbackAsync(); 
                return false;
            }
            await beginTransaction.CommitAsync();
            return true;
        }
    }
}
