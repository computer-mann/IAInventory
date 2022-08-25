namespace WebApi.Models.ViewModels
{
    public class ProductViewModel
    {
        public double Price { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitsInStock { get; set; }
        public string Image { get; set; }
        public int? CatId { get; set; }
        public int id { get; set; }

    }
}
