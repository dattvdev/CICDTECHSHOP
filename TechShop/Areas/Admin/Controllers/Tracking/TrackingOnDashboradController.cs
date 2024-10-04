using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechShop.Core.Data;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.Controllers.Tracking
{
    [Area("Admin")]
    [Route("Admin/Tracking/")]
    [Authorize(Roles = "Admin")]
    public class TrackingOnDashboradController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly ITrackingRepository _trackingRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly TechshopDbContext _context;
        public TrackingOnDashboradController(TechshopDbContext context,
            IProductRepository productRepository,
            IBrandRepository brandRepository, 
            ICustomerRepository customerRepository, 
            ICollaboratorRepository collaboratorRepository, 
            ITrackingRepository trackingRepository,
            ICommentRepository commentRepository
            )
        {
            _context = context;
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _customerRepository = customerRepository;
            _collaboratorRepository = collaboratorRepository;
            _trackingRepository = trackingRepository;
            _commentRepository = commentRepository;
        }
        [HttpGet("CountingStatistics")]
        public async Task<JsonResult> CountingStatistics()
        {
            var countProducts = _productRepository.CountProducts();
            var countBrands = _brandRepository.CountBrands();
            var countCustomters = await _customerRepository.Count();
            var countCollaborators = await _collaboratorRepository.Count();
            var customerJustOrdered = await _customerRepository.getCustomerJustByNewProduct();
            var topRateProduct = await _commentRepository.GetTopProductWithCommentVote();

            var cusotmerHighLoyaltyPoint = await _context.Customers.Include(i => i.User).OrderByDescending(u => u.LoyaltyPoints).Select(c => new
            {
                userName = c.User.FullName,
                loyaltyPoint = c.LoyaltyPoints,
            }).FirstOrDefaultAsync();


            return Json(new
            {
                countProducts = countProducts,
                countBrands = countBrands,
                countCustomters = countCustomters,
                countCollaborators = countCollaborators,
                customerJustOrdered = customerJustOrdered,
                topRateProduct = topRateProduct,
                cusotmerHighLoyaltyPoint = cusotmerHighLoyaltyPoint,
            });
        }
        [HttpGet("DAChartInvoices")]
        public async Task<JsonResult> DAChartInvoices()
        {
            var result = await _trackingRepository.DAChartInvoices();
            return Json(result);
        }
        [HttpGet("DAChartProductSoldAndNotSold")]
        public async Task<JsonResult> DAChartProductSoldAndNotSold()
        {
            var result = await _trackingRepository.DAChartProductSoldAndNotSold();
            return Json(result);
        }
        [HttpGet("DAChartProductSoldInMonth")] 
        public async Task<JsonResult> DAChartProductSoldInMonth()
        {
            var result = await _trackingRepository.DAChartProductHaveSoldInMonth();
            return Json(result);
        }
        [HttpGet("GetCollaboratorsInDashBoards")]
        public async Task<JsonResult> GetCollaboratorsInDashBoards()
        {
            var result = await _trackingRepository.GetCollaboratorsInDashBoards();
            return Json(result);
        }
        [HttpGet("DAChartTopSellingProducts")]
        public async Task<JsonResult> GetChartTopSellingProducts()
        {
            var result = await _trackingRepository.GetChartTopSellingProducts();
            return Json(result);
        }

    }
}
