using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Product
    {
        [Key]
        public Guid? Id { get; set; }
        public Guid? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public Guid? CategoryId { get; set; }
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Range(1, 100, ErrorMessage = "Discount invalid")]
        public int? Discount { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
        public string? Image { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9]+(-[a-zA-Z0-9]+)*$", ErrorMessage = "UrlSlug must have segments separated by hyphens.")]
        public string? UrlSlug { get; set; }
        public virtual List<ProductImage>? Images { get; set; }
        public virtual List<ProductHardware>? ProductHardwares { get; set; }
        public virtual List<Filter>? Filters { get; set; }
        public virtual List<ProductFilter>? ProductFilters { get; set; }
		public virtual List<Comment>? Comments { get; set; }
		public virtual Category? Category { get; set; }

    }
}
