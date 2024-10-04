using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class ProductColor
    {
        [Key] 
        public Guid? Id { get; set; }
        public virtual Guid? ProductHardwareId { get; set; }
        public ProductHardware? ProductHardware { get; set; }
        [StringLength(100, ErrorMessage = "RGB cannot be longer than 100 characters.")]
        public string? RGB { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Price must greater than 0")]
        public double Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must greater than 0")]
        public int Quantity { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
        public virtual List<Customer>? Customers { get; set; }
        public virtual List<Order>? Orders { get; set; }
        public virtual List<InvoiceProductColors>? InvoiceProducts { get; set; }
        public virtual List<Invoice>? Invoices { get; set; }
    }
}
