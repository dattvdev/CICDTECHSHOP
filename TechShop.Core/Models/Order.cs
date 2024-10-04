using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Order
    {
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public Guid? ProductColorId { get; set; }
        public ProductColor? ProductColor { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quanitity must greater than 0")]
        public int Quanitity { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
        public virtual List<Order>? Orders { get; set; }
    }
}
