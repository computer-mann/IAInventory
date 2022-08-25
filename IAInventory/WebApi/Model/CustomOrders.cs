using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SuitStore.Areas.Identity.Models;


namespace SuitStore.Models.Store{

	[Table(name: "CustomOrders")]
	public class CustomOrders{

		[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string CustomOrderId{ get; set; }

		public string CustomerId {get; set;}

		[ForeignKey("CustomerId")]
		public Customer Customer {get; set; }

		[DataType(DataType.Text)]
		public string Description{ get; set; }

		public string Image{get; set;}
		
		[Timestamp]
        public byte[] Timestamp {get; set;}
	}
}