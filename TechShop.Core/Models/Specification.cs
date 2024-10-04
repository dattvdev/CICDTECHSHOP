using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Specification
    {
        [Key]
        public Guid? Id { get; set; }
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }
        public virtual List<Filter>? Filters { get; set; }
        public virtual Category? Category { get; set; }
    }
}
