using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class InvoiceProductColors
    {
        public Guid? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
        public Guid? ProductColorsId { get; set; }
        public ProductColor? ProductColor { get; set; }
        [Range(1, double.MaxValue)]
        public double? ProductPrice { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quanitity must greater than 0")]
        public int? Quantity {  get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
    }
}
