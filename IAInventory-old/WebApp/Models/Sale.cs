using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table(name: "Sale")]
    public class Sale
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AttendantName { get; set; }
        public string Customername { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }
     
        public List<SaleDetail> OrderDetails { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(6,2)")]
        public double OrderTotal { get; set; }
        public double AmountCustomerPaid { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
