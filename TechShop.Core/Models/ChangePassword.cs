using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Old password is required.")]
        public string OldPassword {  get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,}$", ErrorMessage = "Password must be at least 6 characters long, contain at least one uppercase letter, one lowercase letter, one special character, one number, and no spaces.")]
        public string NewPassword {  get; set; }

        [Required(ErrorMessage = "Please confirm your new password.")]
        public string ConfirmNewPassword { get; set; }
    }
}
