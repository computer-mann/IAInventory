using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    [Table(name: "Category")]
    public class Category
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [DataType(DataType.Text), MaxLength(50)]
        public string CategoryName { get; set; }
        public string Description { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

    }
}
