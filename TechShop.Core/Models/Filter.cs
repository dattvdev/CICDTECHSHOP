using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Filter
    {
        [Key]
        public Guid? Id { get; set; }
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }

        public Guid? SpecificationId { get; set; }
        public Category? Category { get; set; }
        public virtual List<Product>? Products { get; set; }
        public virtual List<ProductFilter>? ProductFilters { get; set; }
        public Specification? Specification { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
    }
}
