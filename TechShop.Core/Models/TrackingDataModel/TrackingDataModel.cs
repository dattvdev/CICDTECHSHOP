using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models.TrackingDataModel
{
    public class TrackingDataModel
    {
        public string? ChartType { get; set; }
        public string? ItemType { get; set; }
        public string? SearchItem { get; set; }
        public int? PageSizeItemType { get; set; }
        public int? CurrentPageItemType { get; set; }
        public int? ChartSize { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public HotRecommendModel? HotRecommendModel { get; set; }
        public List<ItemSelectedModel>? ItemSelected { get; set; }
    }
}
