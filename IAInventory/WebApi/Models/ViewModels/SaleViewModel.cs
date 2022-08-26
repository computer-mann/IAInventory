using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.ViewModels
{
    public class SaleViewModel
    {
        [Required]
        public string CustomerName { get; set; }
        public List<SaleDetailViewModel> Details { get; set; }
        [Required]
        public double TotalCost { get; set; }
        [Required]
        public DateTime DateofSale { get; set; }
        [Required]
        public double AmountCustomerPaid { get; set; }
        [Required]
        public int TillId { get; set; }

    }

    public class SaleDetailViewModel
    {
        [Required]
        public double UnitCost { get; set; }
        [Required]
        public int Units { get; set; }
        [Required]
        public int ProductId { get; set; }
        
    }
}
