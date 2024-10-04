using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class ProductImage
    {
        [Key]
        public Guid? Id { get; set; }
        public virtual Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        [Range(1, 2, ErrorMessage = "Type must be either 1 (Slide) or 2 (Content).")]
        public int? Type {  get; set; } // 1: Slide | 2: Content
        [Required]
        public string? UrlImage { get; set; }
        public string? Alt { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated At")]
        public DateTime? UpdatedAt { get; set; }
    }
}
