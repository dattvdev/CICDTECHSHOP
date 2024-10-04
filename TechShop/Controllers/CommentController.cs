using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using TechShop.Core.Models;
using TechShop.Core.Repositories;

namespace TechShop.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentRepository commentRepository, UserManager<User> userManager)
        {
            _commentRepository = commentRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> GetCommentInDetailProduct_First(string id)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var resuls = await _commentRepository.GetCommentInDetailProduct_First(id, userId);
            return Json(resuls);
        }

        public async Task<IActionResult> GetCommentInDetailProduct_Second(string? productId, int? nextPage)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var resuls = await _commentRepository.GetCommentInDetailProduct(productId, nextPage, userId);
            if(resuls is null)
                return Json(new
                {
                    success = false,
                });
            return Json(new
            {
                success = true,
                data = resuls
            });
        }

        public async Task<IActionResult> CheckUserBuyProduct(string productId)
        {
			var userId = _userManager.GetUserId(HttpContext.User);

            var check = await _commentRepository.CheckUserHaveCommments(productId, userId);
            if (check is not null)
            {
                return Json(new
                {
                    success = false,
                    message = "You have comment at this product"
                });
            }
            else
            {
                return Json(new
                {
                    success = true,
                    message = "Allow comment"
                });
            }
        }

        public async Task<IActionResult> GetCommentReplyInDetailProduct(string? productId, string? commentId ,int? nextPage)
        {
            if (productId is null || commentId is null || nextPage is null)
                return BadRequest();
            var resuls = await _commentRepository.GetCommentInDetailProduct_Reply(productId, commentId, nextPage);
            if (resuls is null)
                return Json(new
                {
                    success = false,
                });
            return Json(new
            {
                success = true,
                data = resuls
            });
        }

        public async Task<IActionResult> UpdateAgainDetailProduct(string? productId)
        {
            var result = await _commentRepository.UpdateAgainDetailProduct(productId);
            return Json(result);
        }

        [HttpGet]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> DeleteComment(string commentId)
        {
            var commentDetail = await _commentRepository.GetCommentWithUserById(commentId);
            if(commentDetail is null)
            {
                return NotFound();
            }
            var userId = _userManager.GetUserId(HttpContext.User);
            if(userId is null)
            {
                return BadRequest();
            }
            if (!commentDetail.UserId.Equals(userId))
            {
                return BadRequest();
            }
            var resuls = await _commentRepository.DeleteComment(commentId);
            return Json(resuls);
        }

        [HttpPost]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> AddComment(Comment comment)
        {
            comment.CreatedAt = DateTime.Now;
            comment.UpdatedAt = DateTime.Now;
            var userId = _userManager.GetUserId(HttpContext.User);
            if(comment.Rate != 0)
            {
                var check = await _commentRepository.CheckUserHaveCommments(comment.ProductId.ToString(), userId);
                if (check is not null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "You have comment at this product"
                    });
                }
                var check2 = await _commentRepository.CheckUserHaveBuyProduct(comment.ProductId.ToString(), userId);
				if (check2 != true)
				{
					return Json(new
					{
						success = false,
						message = "You must by the product to review this product"
					});
				}
			}

            var newComment = await _commentRepository.AddComment(comment);
            if (newComment is null)
                return Json(new
                {
                    success = false,
                    message = "faild add new comment"
                });
            return Json(
                new 
                { 
                    success = true,
                    data = newComment
                }
            );
        }
    }
}
