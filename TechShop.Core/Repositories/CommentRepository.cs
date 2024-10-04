using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using TechShop.Core.Data;
using TechShop.Core.Models;
using TechShop.Core.Models.DataTableModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Order = TechShop.Core.Models.DataTableModel.Order;

namespace TechShop.Core.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly TechshopDbContext _context;
        public CommentRepository(TechshopDbContext context)
        {
            _context = context;
        }

        public async Task<object> AddComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            var newComment = await _context.Comments
                .Where(c => c.Id == comment.Id)
                .Select(c => new
                {
                    commentId = c.Id,
                    customerId = c.UserId,
                    customerName = c.User.FullName,
                    createAt = c.CreatedAt,
                    rate = c.Rate,
                    commentText = c.CommentText,
                    isReplies = c.UserReplies.Any(),
                    repForUser = c.UserReply.User.FullName
                })
                .FirstOrDefaultAsync();

            return newComment;
        }

        public async Task<object> DeleteComment(string id)
        {
            var comment = await _context.Comments.Include(c => c.UserReplies).FirstOrDefaultAsync(c => c.Id.ToString().Equals(id));
            if (comment == null)
            {
                return new { success = false, message = "Product not found." };
            }
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var userReplies = comment.UserReplies;
                if (userReplies is not null && userReplies.Count > 0)
                {
                    async Task DeleteReplies(Comment? comment)
                    {
                        var nestedReply = await _context.Comments
                             .Include(c => c.UserReplies)
                             .FirstOrDefaultAsync(c => c.Id.ToString().Equals(comment.Id.ToString()));

                        if (nestedReply.UserReplies is not null && nestedReply.UserReplies.Count > 0)
                        {
                            foreach (var reply in comment.UserReplies)
                            {
                                await DeleteReplies(reply);
                            }
                        }
                        _context.Comments.Remove(nestedReply);
                    }

                    foreach (var item in userReplies)
                    {
                        await DeleteReplies(item);
                        _context.Comments.Remove(item);
                    }
                }
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new { success = false, message = ex.Message };
            }
            await transaction.CommitAsync();
            return new { success = true, message = "Product deleted successfully.", id = id };
        }

        public async Task<Comment?> GetCommentWithUserById(string id)
        {
            var comment = await _context.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.Id.ToString().Equals(id));
            return comment;
        }

        public async Task<object> GetCommentInDetailProduct(string? productId, int? nextPage, string? userId)
        {
            var query = _context.Comments
                .Where(c => c.ProductId.ToString().Equals(productId) && c.Rate.HasValue && c.Rate != 0);

            if (string.IsNullOrEmpty(userId))
            {
                query = query.OrderByDescending(c => c.Rate).ThenByDescending(c => c.CreatedAt);
            }
            else
            {
                query = query.OrderByDescending(c => c.UserId == userId)
                            .ThenByDescending(c => c.Rate)
                            .ThenByDescending(c => c.CreatedAt);
            }

            var actualNextPage = nextPage ?? 1; 
            var pageSize = 1; 
            var paginatedComments = await query
                .Skip((actualNextPage - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new
                {
                    commentId = c.Id,
                    customerId = c.UserId,
                    customerName = c.User.FullName,
                    createAt = c.CreatedAt,
                    rate = c.Rate,
                    commentText = c.CommentText,
                    isReplies = c.UserReplies.Any() 
                })
                .ToListAsync();

            return paginatedComments;
        }

        public async Task<object?> GetCommentInDetailProduct_First(string? productId, string? userId)
        {
            var query = _context.Comments
                .Where(c => c.ProductId.ToString().Equals(productId) && c.Rate.HasValue && c.Rate != 0)
                .GroupBy(m => m.ProductId);

            var result = await query.Select(m => new
            {
                star5 = m.Count(c => c.Rate == 5),
                percent5 = (double)m.Count(c => c.Rate == 5) / m.Count() * 100,
                star4 = m.Count(c => c.Rate == 4),
                percent4 = (double)m.Count(c => c.Rate == 4) / m.Count() * 100,
                star3 = m.Count(c => c.Rate == 3),
                percent3 = (double)m.Count(c => c.Rate == 3) / m.Count() * 100,
                star2 = m.Count(c => c.Rate == 2),
                percent2 = (double)m.Count(c => c.Rate == 2) / m.Count() * 100,
                star1 = m.Count(c => c.Rate == 1),
                percent1 = (double)m.Count(c => c.Rate == 1) / m.Count() * 100,
                totalStar = Math.Round((decimal)m.Sum(c => c.Rate)/(decimal)m.Count(), 2) ,
                totalComment = m.Count(),
                comments = (userId == null)
                    ? m.OrderByDescending(mc => mc.Rate).ThenByDescending(mc => mc.CreatedAt).Select(c => new
                    {
                        commentId = c.Id,
                        customerId = c.UserId,
                        customerName = c.User.FullName,
                        createAt = c.CreatedAt,
                        rate = c.Rate,
                        commentText = c.CommentText,
                        isReplies = c.UserReplies.Count() > 0
                    }).Take(1).ToList()
                    : m.OrderByDescending(c => c.UserId == userId)
                        .ThenByDescending(c => c.Rate)
                        .ThenByDescending(c => c.CreatedAt) 
                        .Select(c => new
                        {
                            commentId = c.Id,
                            customerId = c.UserId,
                            customerName = c.User.FullName,
                            createAt = c.CreatedAt,
                            rate = c.Rate,
                            commentText = c.CommentText,
                            isReplies = c.UserReplies.Count() > 0
                        }).Take(1).ToList(),
            }).FirstOrDefaultAsync();
            return result;
        }


        public async Task<object?> GetCommentInDetailProduct_Reply(string? productId, string? commentId, int? nextPage)
        {
            if (string.IsNullOrWhiteSpace(productId) || string.IsNullOrWhiteSpace(commentId))
            {
                return null;
            }

            var query = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.UserReplies)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(c => c.Id.ToString().Equals(commentId));

            if (query is not null)
            {
                var allReplies = new List<Comment>();

                async Task CollectReplies(Comment? comment)
                {
                    if (comment?.UserReplies != null)
                    {
                        foreach (var reply in comment.UserReplies)
                        {
                            allReplies.Add(reply);

                            var nestedReply = await _context.Comments
                                .Include(c => c.UserReplies)
                                .ThenInclude(c => c.User)
                                .FirstOrDefaultAsync(c => c.Id.ToString().Equals(reply.Id.ToString()));

                            if (nestedReply?.UserReplies is not null)
                            {
                                await CollectReplies(nestedReply);
                            }
                        }
                    }
                }

                await CollectReplies(query);

                var actualNextPage = nextPage ?? 1;
                var pageSize = 1;

                var paginatedComments = allReplies
                    .OrderBy(c => c.CreatedAt)
                    .Skip((actualNextPage - 1) * pageSize)
                    .Take(pageSize)
                    .Select(c => new
                    {
                        commentId = c.Id,
                        customerId = c.UserId,
                        customerName = c.User?.FullName,
                        createAt = c.CreatedAt,
                        rate = c.Rate,
                        commentText = c.CommentText,
                        repForUser = c.UserReply?.User?.FullName
                    });

                return paginatedComments;
            }

            return null;
        }


        public async Task<object> GetDataComments(DataTableParameters parameters, string? productId)
        {
            // Check order
            if (parameters.Order == null || !parameters.Order.Any())
            {
                parameters.Order = new Order[] { new Order { Column = 0, Dir = "asc" } };
            }

            // Take list comments from database
            var comments = _context.Comments.AsQueryable();

            // handle filter product id
            if (!string.IsNullOrEmpty(productId))
            {
                comments = comments.Where(c => c.ProductId.ToString().Equals(productId));
            }

            // mapping data
            var query = comments
                .Select(i => new
                {
                    id = i.Id,
                    user = i.User.FullName,
                    commentAt = i.Product.Name,
                    rate = !i.Rate.HasValue ? "" : i.Rate == 1 ? "⭐" : i.Rate == 2 ? "⭐⭐" : i.Rate == 3 ? "⭐⭐⭐" : i.Rate == 4 ? "⭐⭐⭐⭐" : i.Rate == 5 ? "⭐⭐⭐⭐⭐" : "Unknown",
                    createAt = i.CreatedAt.HasValue
                        ? $"{i.CreatedAt.Value.Day}/{i.CreatedAt.Value.Month}" +
                        $"" +
                        $"/{i.CreatedAt.Value.Year}"
                        : "Unknown",
                    createAtDate = i.CreatedAt.HasValue ? i.CreatedAt.Value : (DateTime?)null
                });

            // search for DataTableParameters 
            if (!string.IsNullOrEmpty(parameters.Search?.Value))
            {
                query = query.Where(i => i.user.Contains(parameters.Search.Value) || i.commentAt.Contains(parameters.Search.Value));
            }

            // sort for DataTableParameters
            var sortColumn = parameters.Order[0].Column;
            var sortDirection = parameters.Order[0].Dir;
            switch (sortColumn)
            {
                case 1:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.user)
                        : query.OrderByDescending(i => i.user);
                    break;
                case 2:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.commentAt)
                        : query.OrderByDescending(i => i.commentAt);
                    break;
                case 3:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.rate.Length)
                        : query.OrderByDescending(i => i.rate.Length);
                    break;
                case 4:
                    query = sortDirection == "asc"
                        ? query.OrderBy(i => i.createAtDate)
                        : query.OrderByDescending(i => i.createAtDate);
                    break;
                default:
                    query = query.OrderBy(i => i.user);
                    break;
            }

            // Count total records
            var totalRecords = await query.CountAsync();
            //Paging
            var data = await query.Skip(parameters.Start).Take(parameters.Length).ToListAsync();

            // end
            var result = new
            {
                draw = parameters.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = data,
            };

            return result;
        }

        public async Task<Comment?> GetDetailComment(string id)
        {
            var result = await _context.Comments
                      .Include(c => c.User)
                      .Include(c => c.UserReplies)
                          .ThenInclude(ur => ur.User)
                      .Include(c => c.UserReplies)
                          .ThenInclude(ur => ur.UserReplies)
                              .ThenInclude(nur => nur.User)
                      .FirstOrDefaultAsync(c => c.Id.ToString().Equals(id));

            return result;
        }

        public async Task<object?> GetTopProductWithCommentVote()
        {
            var topRateProduct = await _context.Comments.Include(c => c.Product).Where(c => c.Rate > 0).Select(i => new
            {
                productId = i.ProductId,
                productName = i.Product.Name,
                rate = i.Rate
            }).GroupBy(p => p.productId).Select(p => new
            {
                productId = p.Key,
                productName = p.Select(p => p.productName).Distinct().First(),
                totalRate = p.Sum(c => c.rate) / p.Count()
            }).OrderByDescending(p => p.totalRate).FirstOrDefaultAsync();

            return topRateProduct;
        }

        public async Task<Comment?> CheckUserHaveCommments(string productId, string userId)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(c => c.ProductId.ToString().Equals(productId) && c.UserId.ToString().Equals(userId) && c.Rate != 0);
            return result;
        }

        public async Task<bool> CheckUserHaveBuyProduct(string productId, string userId)
        {
            var result = await _context.invoices.Include(c => c.InvoiceProducts)
                .Where(c => c.Customer.UserID.ToString().Equals(userId))
                .SelectMany(c => c.ProductColors)
                .Select(c => c.ProductHardware.ProductId)
                .Distinct()
                .AnyAsync(c => c.ToString().Equals(productId));

            return result;
        }

        public async Task<object> UpdateAgainDetailProduct(string? productId)
        {
            var query = _context.Comments
             .Where(c => c.ProductId.ToString().Equals(productId) && c.Rate.HasValue && c.Rate != 0)
             .GroupBy(m => m.ProductId);

            var result = await query.Select(m => new
            {
                star5 = m.Count(c => c.Rate == 5),
                percent5 = (double)m.Count(c => c.Rate == 5) / m.Count() * 100,
                star4 = m.Count(c => c.Rate == 4),
                percent4 = (double)m.Count(c => c.Rate == 4) / m.Count() * 100,
                star3 = m.Count(c => c.Rate == 3),
                percent3 = (double)m.Count(c => c.Rate == 3) / m.Count() * 100,
                star2 = m.Count(c => c.Rate == 2),
                percent2 = (double)m.Count(c => c.Rate == 2) / m.Count() * 100,
                star1 = m.Count(c => c.Rate == 1),
                percent1 = (double)m.Count(c => c.Rate == 1) / m.Count() * 100,
                totalStar = Math.Round((decimal)m.Sum(c => c.Rate) / (decimal)m.Count(), 2),
                totalComment = m.Count()
            }).FirstOrDefaultAsync();
            return result;
        }
    }
}
