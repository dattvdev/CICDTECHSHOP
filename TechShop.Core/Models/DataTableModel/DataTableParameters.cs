namespace TechShop.Core.Models.DataTableModel
{
    public class DataTableParameters
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public Search Search { get; set; }
        public Order[] Order { get; set; }
    }
}
