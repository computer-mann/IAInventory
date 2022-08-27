namespace WebApp.ViewModel
{
    public class CreateSaleViewModel
    {
        public string AttendantName { get; set; }
        public string CustomerName { get; set; }
        public string Products{ get; set; }
        public double AmountCustomerPaid { get; set; }
        public int Quantity { get; set; }

    }
     public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int QauntityBought { get; set; }

    }
}
