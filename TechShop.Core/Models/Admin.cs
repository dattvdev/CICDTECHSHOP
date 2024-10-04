using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class Admin
    {
        [Key]
        public Guid? ID { get; set; }
        public string? UserID { get; set; }
        public User? User { get; set; }
        [Required(ErrorMessage = "Access Level is required.")]
        [Range(1, 10, ErrorMessage = "Access Level must be between 1 and 10.")]
        public int? AccessLevel { get; set; }
    }
}
