using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Core.Models
{
    public class VerifyOtp
    {
        [Required(ErrorMessage = "Mã OTP không được để trống.")]
        public string OtpDigit1 { get; set; }

        [Required(ErrorMessage = "Mã OTP không được để trống.")]
        public string OtpDigit2 { get; set; }

        [Required(ErrorMessage = "Mã OTP không được để trống.")]
        public string OtpDigit3 { get; set; }

        [Required(ErrorMessage = "Mã OTP không được để trống.")]
        public string OtpDigit4 { get; set; }

        [Required(ErrorMessage = "Mã OTP không được để trống.")]
        public string OtpDigit5 { get; set; }

        [Required(ErrorMessage = "Mã OTP không được để trống.")]
        public string OtpDigit6 { get; set; }

    }


}
