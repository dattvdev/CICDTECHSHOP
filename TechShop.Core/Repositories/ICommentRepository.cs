using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;

namespace TechShop.Core.Repositories
{
    public interface ICommentRepository
    {
        public Task<Comment> GetDetailComment(string id);
        public Task<object> DeleteComment(string id);
        public Task<object> GetDataComments(DataTableParameters parameters, string? productId);
        public Task<object> GetTopProductWithCommentVote();
        public Task<object> GetCommentInDetailProduct_First(string? productId, string? userId);
        public Task<object> GetCommentInDetailProduct(string? productId, int? nextPage, string? userId);
        public Task<object> GetCommentInDetailProduct_Reply(string? productId, string? commentId, int? nextPage);
        public Task<object> AddComment(Comment comment);
        public Task<Comment> GetCommentWithUserById(string id);
        public Task<Comment> CheckUserHaveCommments(string productId, string userId);
        public Task<object> UpdateAgainDetailProduct(string? productId);
        public Task<bool> CheckUserHaveBuyProduct(string productId, string userId);

	}
}
