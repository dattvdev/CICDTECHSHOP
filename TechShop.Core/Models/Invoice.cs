using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Invoice
    {
        [Key]
        public Guid? Id { get; set; }
        public Guid? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        [Range(1,3, ErrorMessage = "Invalid Status")]
        public int? Status { get; set; } // 1: Prepare| 2: Delivering| 3: Complete
        [StringLength(100, ErrorMessage = "Method paymment cannot be longer than 100 characters.")]
        public string? MethodPaymment {  get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Telephone { get; set; }
        [Required]
        public string? Address { get; set; }
        public string? Note { get; set; }
        public double? Total { get; set; }
        public virtual List<InvoiceProductColors>? InvoiceProducts { get; set; }
        public virtual List<ProductColor>? ProductColors { get; set; }
        [AllowNull]
        public Guid? CollaboratorId { get; set; }
        public virtual Collaborator? Collaborator { get; set; }  
    }
}
