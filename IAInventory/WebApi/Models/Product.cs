using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace WebApi.Models.Store{

        [Table(name: "Product")]
	public class Product
        {

	[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductId {get; set;}

        public string ProductName { get; set;}

	    public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [DataType(DataType.Currency),Column(TypeName="decimal(6,2)")]
        public double CurrentPrice { get; set; }

        public string Description { get; set; }

        public ProductImage ProductImage { get; set; }

        public int UnitsInStock { get; set; }
        
        [Timestamp]
        public byte[] Timestamp {get; set;}
        
	}
}