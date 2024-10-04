using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public  class ProductWithMinPrice
    {
        public string Name { get; set; }
        public Guid? CateId { get; set; }
        public string CateName { get; set; }
        public int Discount { get; set; }
        public string Image {  get; set; }
        public string UrlSlug { get; set; }
        public double? MinPrice { get; set; }
    }
}
