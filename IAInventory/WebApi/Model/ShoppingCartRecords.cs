using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SuitStore.Models.Store;
using SuitStore.Areas.Identity.Models;

//The data here will be just for before the user checks out his/her cart
//I should put this in a cache
namespace SuitStore.Models.Store{

        [Table(name: "ShoppingCartRecords")]
	public class ShoppingCartRecords{

	[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        public string ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Products Products { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Currency),Column(TypeName="decimal(6,2)")]
        public decimal LineItemTotal { get; set; }

	[Timestamp]
        public byte[] Timestamp {get; set;}

		
	}
}