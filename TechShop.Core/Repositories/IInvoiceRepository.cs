using System.Linq.Expressions;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using TechShop.Core.Models.InvoiceParamesters;

namespace TechShop.Core.Repositories
{
    public interface IInvoiceRepository
    {
        public Task<object> GetInvoiceDetailsAsync(DataTableParameters parameters, int? status, DateTime? startDate, DateTime? endDate);
        public Task<object> GetInvoiceForCollaborator(DataTableParameters parameters, string userId);
        public Task<object> GetProductInInvoice(string id);
        public Task<object> GetDAUserShoppingInMonth(string userId, string invoiceId);
        public Task<Invoice> GetDetailInvoice(string id);
        public Task<List<Invoice>> GetAllInvoices();
        public Task<List<Invoice>> GetAllInvoicesByMonth(DateTime dateTime);
        public Task<object> CancelInvoice(string id);
        public Task<List<Invoice>> GetInvoiceWithOptions(InvoiceParamesterOption invoiceParamesterOption);
        public Task<bool> UpdateInvoiceWhoTakeCare(string invoiceId, string collarId);
        public Task<bool> UpdateInvoiceStatus(string invoiceId, string collarId);
        public Task<object> GetInvoiceDetailsAsync_Client(DataTableParameters parameters, string userId);
    }
}
