using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Brand
    {
        [Key]
        public Guid? Id { get; set; }
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]+(-[a-zA-Z0-9]+)*$", ErrorMessage = "UrlSlug must have segments separated by hyphens.")]
        public string? UrlSlug { get; set; }
        public string? Description { get; set; }
        public virtual List<Product>? Products { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
    }
}
