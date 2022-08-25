using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SuitStore.Models.Store;


namespace SuitStore.Models.Store{

  [Table(name: "ProductImages")]
	public class ProductImages
       {

	      [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ImageId {get; set; }

        public string ProductId {get; set;}

        [ForeignKey("ProductId")]
        public Products Product {get; set;}

        public string Imageurl {get; set;}
        
        [Timestamp]
        public byte[] Timestamp {get; set;}
        
       }
}