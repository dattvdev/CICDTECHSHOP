using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Customer
    {
        [Key]
        public Guid? ID { get; set; }
        public string? UserID { get; set; }

        public User? User { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Loyalty Points must be a positive number.")]
        public int? LoyaltyPoints { get; set; }
        public virtual List<ProductColor>? ProductColors { get; set; }
        public virtual List<Invoice>? Invoices { get; set; }
        public virtual List<Order>? Orders { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
    }
}
