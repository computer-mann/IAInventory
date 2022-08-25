using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace WebApi.Models.Store{

  [Table(name: "ProductImage")]
	public class ProductImage
       {

	      [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageId {get; set; }

        public string ProductId {get; set;}

        [ForeignKey("ProductId")]
        public Product Product {get; set;}

        public string Imageurl {get; set;}
        
        [Timestamp]
        public byte[] Timestamp {get; set;}
        
       }
}