using WebApi.Models.Store;

namespace WebApi.Models.ViewModels
{
    public class DailyReportViewModel
    {
        public double TotalSales { get; set; }
        public double TotalQuantities { get; set; }
        public List<Product> Product { get; set; }
    }
}
