using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechShop.Core.Models.TrackingDataModel;
using TechShop.Core.Repositories;

namespace TechShop.Areas.Admin.Controllers.Tracking
{
    [Area("Admin")]
    [Route("Admin/TrackingData/{action}")]
    [Authorize(Roles = "Admin")]
    public class TrackingDataController : Controller
    {
        private readonly ITrackingRepository _trackingRepository;
        public TrackingDataController(ITrackingRepository trackingRepository) 
        {
            _trackingRepository = trackingRepository;
        }
        [HttpPost]
        public async Task<IActionResult> GetItem(TrackingDataModel dataModel)
        {
            var result = await _trackingRepository.GetItem(dataModel);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> DAChartData(TrackingDataModel dataModel)
        {
            var result = await _trackingRepository.DAChartData(dataModel);
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> DAChartProductWithDateRange(TrackingDataModel dataModel)
        {
            var result = await _trackingRepository.DAChartProductWithDateRange(dataModel);
            return Json(result);
        }
    }
}
