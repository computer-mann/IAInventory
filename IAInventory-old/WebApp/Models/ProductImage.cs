using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table(name: "ProductImage")]
    public class ProductImage
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public string Imageurl { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }
}
