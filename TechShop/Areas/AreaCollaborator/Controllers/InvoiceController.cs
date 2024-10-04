using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using TechShop.Core.Repositories;

namespace TechShop.Areas.AreaCollaborator.Controllers
{
    [Authorize(Roles = "Collaborator")]
    [Area("AreaCollaborator")]
    public class InvoiceController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICollaboratorRepository _collaboratorRepository;
        public InvoiceController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IInvoiceRepository invoiceRepository,
            ICollaboratorRepository collaboratorRepository)
        {
            _userManager = userManager;
            _signinManager = signInManager;
            _invoiceRepository = invoiceRepository;
            _collaboratorRepository = collaboratorRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetInvoiceForCollaborator(DataTableParameters parameters)
        {
            var user = await _userManager.GetUserAsync(User);
            var collar = _collaboratorRepository.GetCollaboratorByUserId(user.Id);
            var result = await _invoiceRepository.GetInvoiceForCollaborator(parameters, collar.ID.ToString());

            return Json(result);
        }

        public async Task<IActionResult> Update(string? id)
        {
            var invoice = await _invoiceRepository.GetDetailInvoice(id);
            return View(invoice);
        }

        public async Task<IActionResult> UpdateInvoiceTakeCare(string? id)
        {
            var invoice = await _invoiceRepository.GetDetailInvoice(id);
            if (invoice is null)
                return BadRequest();
            var user = await _userManager.GetUserAsync(User);
            var collar = _collaboratorRepository.GetCollaboratorByUserId(user.Id);
            var resuls = await _invoiceRepository.UpdateInvoiceWhoTakeCare(id, collar.ID.ToString());
            if(resuls == false)
            {
                return Json(new
                {
                    isSuccess = false,
                    message = "Take care failed"
                });
            }
            return Json(new
            {
                isSuccess = true,
                message = "Take care success"
            });
        }

        public async Task<IActionResult> UpdateStatusForCollar(string id)
        {
            var invoice = await _invoiceRepository.GetDetailInvoice(id);
            if (invoice is null)
                return BadRequest();
            var user = await _userManager.GetUserAsync(User);
            var collar = _collaboratorRepository.GetCollaboratorByUserId(user.Id);
            var result = await _invoiceRepository.UpdateInvoiceStatus(id, collar.ID.ToString());

            return Json(new
            {
                isSuccess = result,
                message = result ? "Update success" : "Update failed"
            });
        }


    }
}
