namespace WebApi.Models.ViewModels
{
    public class SaleViewModel
    {
        
        public string CustomerName { get; set; }
        public List<SaleDetailViewModel> Details { get; set; }
        public double TotalCost { get; set; }
        public DateTime DateofSale { get; set; }
        public double AmountCustomerPaid { get; set; }

    }

    public class SaleDetailViewModel
    {
        public double UnitCost { get; set; }
        public int Units { get; set; }
        public int ProductId { get; set; }
        
    }
}
