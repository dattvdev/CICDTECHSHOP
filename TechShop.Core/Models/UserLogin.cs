using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class UserLogin
    {
        [Required]
        [RegularExpression(@"^\S+@\S+\.\S+$", ErrorMessage = "Email cannot contain whitespace.")]
        public string Email {  get; set; }

        [Required]
        public string Password { get; set; }
    }
}
