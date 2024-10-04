using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class ResetPassword
    {
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$", ErrorMessage = "Password must be at least 6 characters long, contain at least one uppercase letter, one lowercase letter, one special character, one number, and no spaces.")]
        public string Password {  get; set; }

        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string RepeatPassword { get; set; }
    }
}
