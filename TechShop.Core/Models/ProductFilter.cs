using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class ProductFilter
    {
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid? FilterId { get; set; }
        public Filter? Filter { get; set; }
    }
}
