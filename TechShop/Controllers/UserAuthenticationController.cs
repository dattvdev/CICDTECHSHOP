using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Repositories;
using static System.Net.WebRequestMethods;

namespace TechShop.Controllers
{
    public class UserAuthenticationController : Controller
    {
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;
		private readonly ICustomerRepository _customerRepository;
		private readonly TechshopDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEmailSender _emailSender;
        public UserAuthenticationController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ICustomerRepository customerRepository,
            TechshopDbContext context,
            IPasswordHasher<User> passwordHasher,
            IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _userManager = userManager;
			_customerRepository = customerRepository;
			_context = context;
			_passwordHasher = passwordHasher;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
        public async Task<IActionResult> Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
				var existingUser = await _context.Users.FirstOrDefaultAsync(e => e.Email == user.Email);
				if (existingUser != null)
				{
                    //Kiểm tra mật khẩu
                    var result = await _signInManager.PasswordSignInAsync(existingUser, user.Password, isPersistent: false, lockoutOnFailure: false);

					if (result.Succeeded)
					{
                        await _userManager.GetUserAsync(User);
                        return Json(new { success = true });
                    }
					else
					{
						ModelState.AddModelError("Password", "Invalid password please enter again.");
					}
				}
				else
				{
					ModelState.AddModelError("Email", "User not found.");
				}
			}

            //return View();
            return Json(new
            {
                success = false,
                errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                )
            });

        }

        [HttpGet]
		public IActionResult Register()
        {
            return View();
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegister model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return Json(new
                    {
                        success = false,
                        errors = ModelState.ToDictionary(
                            kv => kv.Key,
                            kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        )
                    });
                }
                if (!model.AgreeTerms)
                {
                    ModelState.AddModelError("AgreeTerms", "Please agree our Terms before Sign In.");
                    return Json(new
                    {
                        success = false,
                        errors = ModelState.ToDictionary(
                            kv => kv.Key,
                            kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        )
                    });
                }

                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

                var otp = new Random().Next(100000, 999999).ToString();
                await _emailSender.SendEmailAsync(model.Email, "Mã xác nhận OTP", $"Mã OTP của bạn là: {otp}");

                HttpContext.Session.SetString("Otp", otp);
                HttpContext.Session.SetString("Email", model.Email);
                HttpContext.Session.SetString("UserName", model.UserName);
                HttpContext.Session.SetString("PasswordHash", model.Password);
                HttpContext.Session.SetString("PhoneNumber", model.PhoneNumber);
                HttpContext.Session.SetString("FullName", model.FullName);

                //return RedirectToAction("VerifyOtpAfterSignUp", new { email = model.Email });
                return Json(new { success = true, redirectUrl = Url.Action("VerifyOtpAfterSignUp", new { email = model.Email }) });
            }
            return Json(new
            {
                success = false,
                errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                )
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("UserId");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPassword user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var forgotPassUser = await _context.Users.FirstOrDefaultAsync(f => f.Email == user.Email);
            if (forgotPassUser == null)
            {
                ModelState.AddModelError("Email", "Email dose not exist!");
                return View(user);
            }

            // Tạo mã OTP (có thể là 6 chữ số ngẫu nhiên)
            var otp = new Random().Next(100000, 999999).ToString();

            HttpContext.Session.SetString("ForgotPasswordEmail", user.Email);
            HttpContext.Session.SetString("ForgotPasswordOTP", otp);

            // Gửi email chứa mã OTP (sử dụng dịch vụ gửi email)
            await _emailSender.SendEmailAsync(user.Email, "Mã xác nhận OTP", $"Mã OTP của bạn là: {otp}");

            // Chuyển đến bước nhập mã OTP
            return RedirectToAction("VerifyOtp", new { email = user.Email });
        }



        [HttpGet]
        public IActionResult VerifyOtp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyOtp(VerifyOtp model)
        {
            if (!ModelState.IsValid)
            {
                //return View(model);
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
            }

            var otp = $"{model.OtpDigit1}{model.OtpDigit2}{model.OtpDigit3}{model.OtpDigit4}{model.OtpDigit5}{model.OtpDigit6}";

            // Lấy mã OTP và email từ Session
            var sessionOtp = HttpContext.Session.GetString("ForgotPasswordOTP");
            var sessionEmail = HttpContext.Session.GetString("ForgotPasswordEmail");

            if (otp.Equals(sessionOtp) && !string.IsNullOrEmpty(sessionEmail))
            {
                // Xóa OTP khỏi session
                HttpContext.Session.Remove("ForgotPasswordOTP");

                //return RedirectToAction("ResetPassword", new { email = sessionEmail });
                return Json(new { success = true, redirectUrl = Url.Action("ResetPassword", new { email = sessionEmail }) });
            }
            
                //ModelState.AddModelError("OtpDigit1", "Mã OTP không chính xác, vui lòng thử lại.");
                return Json(new { success = false, message = "Mã OTP không hợp lệ." });
            
        }

        [HttpGet]
        public IActionResult VerifyOtpAfterSignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyOtpAfterSignUp(VerifyOtp model)
        {

            if (!ModelState.IsValid)
            {
                //return View(model);
                return Json(new { success = false, message = "Dữ liệu không hợp lệ" });
            }

            var otp = $"{model.OtpDigit1}{model.OtpDigit2}{model.OtpDigit3}{model.OtpDigit4}{model.OtpDigit5}{model.OtpDigit6}";

            // Lấy OTP và Email đã lưu trong Session
            var storedOtp = HttpContext.Session.GetString("Otp");
            var storedEmail = HttpContext.Session.GetString("Email");

            if (otp.Equals(storedOtp) && !string.IsNullOrEmpty(storedEmail))
            {
                // Xóa OTP và email khỏi session
                HttpContext.Session.Remove("Otp");

                var userName = HttpContext.Session.GetString("UserName");
                var passwordHash = HttpContext.Session.GetString("PasswordHash");
                var phone = HttpContext.Session.GetString("PhoneNumber");
                var fullName = HttpContext.Session.GetString("FullName");

                var user = new User
                {
                    UserName = userName,
                    Email = storedEmail,
                    PasswordHash = passwordHash,
                    PhoneNumber = phone,
                    FullName = fullName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    EmailConfirmed = true // Xác nhận email
                };

                var result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (result.Succeeded)
                {
                    // Gán role "Customer" cho người dùng
                    await _userManager.AddToRoleAsync(user, "Customer");

                    // Tạo bản ghi trong bảng Customer
                    var customer = new Customer
                    {
                        ID = Guid.NewGuid(),
                        UserID = user.Id,
                        LoyaltyPoints = 0,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();

                    // Xóa thông tin OTP khỏi Session
                    HttpContext.Session.Remove("Otp");
                    HttpContext.Session.Remove("Email");
                    HttpContext.Session.Remove("UserName");
                    HttpContext.Session.Remove("PasswordHash");
                    HttpContext.Session.Remove("PhoneNumber");

                    // Chuyển hướng đến trang đăng nhập
                    return Json(new { success = true, redirectUrl = Url.Action("Login", "UserAuthentication") });
                }
            }
            //return View(model);
            return Json(new { success = false, message = "Mã OTP không hợp lệ." });
        }


        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            // Kiểm tra xem email có tồn tại trong session không
            var email = HttpContext.Session.GetString("ForgotPasswordEmail");
            if (string.IsNullOrEmpty(email))
            {
                //return RedirectToAction("ForgotPassword", "UserAuthentication");
                return Json(new { success = false, message = "Email không hợp lệ." });
            }

            // Kiểm tra tính hợp lệ của model
            if (ModelState.IsValid)
            {
                // Tìm người dùng theo email
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user != null)
                {
                    // Hash mật khẩu mới
                    user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

                    // Cập nhật mật khẩu vào cơ sở dữ liệu
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    // Xóa email khỏi session
                    HttpContext.Session.Remove("ForgotPasswordEmail");

                    // Đăng nhập người dùng
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Chuyển hướng đến trang đăng nhập hoặc trang chính
                    //return RedirectToAction("Login", "UserAuthentication");
                    return Json(new { success = true, redirectUrl = Url.Action("Login", "UserAuthentication") });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return Json(new
            {
                success = false,
                errors = ModelState.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                )
            });
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Lấy thông tin người dùng hiện tại
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "UserAuthentication");
            }

            // Kiểm tra mật khẩu cũ có chính xác không
            var isOldPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.OldPassword);
            if (!isOldPasswordCorrect)
            {
                ModelState.AddModelError("OldPassword", "Mật khẩu cũ không đúng.");
                return View(model);
            }

            // Kiểm tra mật khẩu mới và xác nhận mật khẩu có giống nhau không
            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("ConfirmNewPassword", "Mật khẩu mới và xác nhận mật khẩu không khớp.");
                return View(model);
            }

            // Thay đổi mật khẩu
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                // Đăng xuất sau khi đổi mật khẩu thành công
                await _signInManager.SignOutAsync();

                // Cập nhật thông tin người dùng trong DB
                user.UpdatedAt = DateTime.Now;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                // Chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login", "UserAuthentication");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [Route("/AccessDenied/")]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }

}

