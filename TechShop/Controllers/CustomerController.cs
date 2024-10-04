using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using TechShop.Core.Repositories;

namespace TechShop.Controllers
{
    public class CustomerController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        private readonly TechshopDbContext _context;
        private readonly IInvoiceRepository _invoiceRepository;

		public CustomerController(UserManager<User> userManager, TechshopDbContext context, SignInManager<User> signinManager, IInvoiceRepository invoiceRepository)
		{

			_userManager = userManager;
			_context = context;
			_signinManager = signinManager;
            _invoiceRepository = invoiceRepository;
		}

		public async Task <IActionResult> Index()
        {
            try
            {
                ViewBag.Active = "InfoPage";
                return View(await _userManager.GetUserAsync(this.User));
            }
            catch
            {
                return NotFound();
            }
        }

        public IActionResult Support()
        {
            ViewBag.Active = "SupportPage";
            return View("Support");
        }

        public async Task<IActionResult> InfoPage()
        {
            ViewBag.Active = "InfoPage";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SupportPage()
        {
            ViewBag.Active = "SupportPage";
            return RedirectToAction("Support", "Customer");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, [Bind("Id, UserName, FullName, Email, PhoneNumber, Address, BirthDay")] User userModel)
        {
            if (id.ToString() != userModel.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                if (user == null || (userModel.Id != user.Id))
                {
                    return NotFound();
                }

                try
                {
					user.UserName = userModel.UserName;
					user.FullName = userModel.FullName;
					user.Email = userModel.Email;
					user.PhoneNumber = userModel.PhoneNumber;
					user.Address = userModel.Address;
					user.BirthDay = userModel.BirthDay;
				}
                catch(Exception ex)
                {
                    throw ex;
                }

                var r =  await _userManager.UpdateAsync(user);
                if (r.Succeeded)
                {
                    await _signinManager.RefreshSignInAsync(user);
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            else
            {
                var errors = ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

                return Json(new { success = false, errors });
            }
        }

        public async Task<IActionResult> ChangePasswordPage()
        {
            ViewBag.Active = "ChangePasswordPage";
            return RedirectToAction("ChangePassword", "Customer");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.Active = "ChangePasswordPage";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            // Check model state early on
            if (!ModelState.IsValid)
            {
                ViewBag.Active = "ChangePasswordPage";
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
                ModelState.AddModelError("OldPassword", "Your password not correct!");
                ViewBag.Active = "ChangePasswordPage";
                return View(model);
                //return RedirectToAction("ChangePassword", "Customer")
            }

            // Kiểm tra mật khẩu mới và xác nhận mật khẩu có giống nhau không
            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("ConfirmNewPassword", "Passwords do not match.");
                ViewBag.Active = "ChangePasswordPage";
                return View(model);
            }

            // Thay đổi mật khẩu
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (result.Succeeded)
            {
                // Đăng xuất sau khi đổi mật khẩu thành công
                await _signinManager.SignOutAsync();

                // Cập nhật thông tin người dùng trong DB
                user.UpdatedAt = DateTime.Now;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                ViewBag.success = true;
                ViewBag.Active = "ChangePasswordPage";
                return View(model);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewBag.Active = "ChangePasswordPage";
            return View(model);
        }
        public IActionResult ShoppingHistory()
        {
            ViewBag.Active = "ShoppingHistoryPage";
            return View("ShoppingHistory");
        }

        public async Task<IActionResult> ShoppingHistoryPage()
        {
            ViewBag.Active = "ShoppingHistoryPage";
            return RedirectToAction("ShoppingHistory", "Customer");
        }

        public async Task<IActionResult> Detail(string id)
        {
            if (id is null)
                return NotFound();

            var invoice = await _invoiceRepository.GetDetailInvoice(id);

            if (invoice is null)
                return NotFound();
            ViewBag.Active = "ShoppingHistoryPage";
            return View(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> GetDataInvoices(DataTableParameters parameters)
        {
            await Task.Delay(500); // để cho xịn xịn chút
            if (parameters is null)
            {
                return BadRequest("Invalid request");
            }
            var id = _userManager.GetUserId(this.User);
            var result = await _invoiceRepository.GetInvoiceDetailsAsync_Client(parameters, id);

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductInInvoice(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid invoice ID.");
            }

            var results = await _invoiceRepository.GetProductInInvoice(id);

            if (results is null)
            {
                return NotFound();
            }
            return Json(results);
            //return Json(new {results = results, Active = "ShoppingHistoryPage"});
        }
    }
}
