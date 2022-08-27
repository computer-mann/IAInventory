using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table(name: "SaleDetail")]
    public class SaleDetail
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SaleId { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public double LineItemCost { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }
}
