using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;
using TechShop.Core.Models.TrackingDataModel;

namespace TechShop.Core.Repositories
{
    public interface ITrackingRepository
    {
        public Task<object> DAChartInvoices();
        public Task<object> DAChartProductSoldAndNotSold();
        public Task<object> DAChartProductHaveSoldInMonth();
        public Task<object> GetCollaboratorsInDashBoards();
        public Task<object> GetChartTopSellingProducts();
        public Task<object> GetItem(TrackingDataModel model);
        public Task<object> DAChartData(TrackingDataModel model);
        public Task<object> DAChartProductWithDateRange(TrackingDataModel model);
    }
}
