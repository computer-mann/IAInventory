using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace WebApi.Models.Store{
        [Table(name: "Sales")]
	public class Sales{

	[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string Id{ get; set; }
        //select max(Orderno) from Orders
        public int OrderNo {get;set;}
        
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ShipDate { get; set; }     

        public bool Delivered { get; set; }

        public List<OrderDetails> OrderDetails { get; set; } 

        [DataType(DataType.Currency),Column(TypeName="decimal(6,2)")]
        public decimal OrderTotal { get; set; }

        [Timestamp]
        public byte[] Timestamp {get; set;}
	}
}