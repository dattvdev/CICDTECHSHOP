using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechShop.Core.Data;
using TechShop.Core.Repositories;
using TechShop.Core.Models.DataTableModel;

namespace TechShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentsController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IProductRepository _productRepository;

        public CommentsController(ICommentRepository commentRepository, IProductRepository productRepository)
        {
            _commentRepository = commentRepository;
            _productRepository = productRepository;
        }
        public IActionResult Index()
        {
            ViewData["products"] = _productRepository.GetProducts();
            return View();
        }
        public async Task<IActionResult> Detail(string id)
        {
            var results = await _commentRepository.GetDetailComment(id);
            if(results is null)
            {
                return NotFound();
            }
            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var retsult = await _commentRepository.DeleteComment(id);

            if(retsult is null)
            {
                return BadRequest("Invaild request");
            }

            return Json(retsult);
        }

        [HttpPost]
        public async Task<IActionResult> GetDataComments(DataTableParameters parameters, [FromQuery] string? productId)
        {
            await Task.Delay(200); // để cho xịn xịn chút
            // Check DataTableParameters 
            if (parameters == null)
            {
                return BadRequest("Invalid parameters");
            }

            var result = await _commentRepository.GetDataComments(parameters, productId);

            return Json(result);
        }

    }
}
