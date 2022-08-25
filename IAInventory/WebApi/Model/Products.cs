using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SuitStore.Models.Store;


namespace SuitStore.Models.Store{

        [Table(name: "Products")]
	public class Products
        {

	[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductId {get; set;}

        public string ProductName { get; set;}

	public string CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Categories Category { get; set; }

        public string BrandId {get; set; }

        [ForeignKey(nameof(BrandId))]
        public CatalogBrand Brand {get; set; }

        public string SKU { get; set; }

        [DataType(DataType.Currency),Column(TypeName="decimal(6,2)")]
        public decimal CurrentPrice { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(TagId))]
        public Tags Tags {get;set;}

        public string TagId{get;set;}

        public List<ProductImages> ProductImages { get; set; }

        public int UnitsInStock { get; set; }
        
        [Timestamp]
        public byte[] Timestamp {get; set;}
        
	}
}