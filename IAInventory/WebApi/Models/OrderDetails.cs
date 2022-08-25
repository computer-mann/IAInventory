using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace WebApi.Models.Store{

        [Table(name: "OrderDetails")]
	public class OrderDetails{

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id {get; set; }

        public string OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Orders Order { get; set; }

        public string CustomerId {get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer {get; set;}

        public string ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Products Products { get; set; }      

        public int Quantity { get; set; }

        [DataType(DataType.Currency),Column(TypeName="decimal(6,2)")]  
        public decimal LineItemCost {get; set;}     
        
        [Timestamp]
        public byte[] Timestamp {get; set;}

	}
}

/*
 [DataType(DataType.Currency)]
 public decimal UnitCost { get; set; }
*/