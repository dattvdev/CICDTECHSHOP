using TechShop.Core.Models;

namespace TechShop.Dtos
{
    public class UpdateProductColorRequest
    {
        public List<ProductColor> ProductColors { get; set; }
        public string ProductHardwareId { get; set; }
        public string ProductHardwareName { get; set; }
    }
}
