using TechShop.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class ProductHardware
    {
        [Key]
        public Guid? Id { get; set; }
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }
        public virtual List<ProductColor>? ProductColors { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
    }
}
