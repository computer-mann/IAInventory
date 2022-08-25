using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace WebApi.Models.Store{
	[Table(name: "Category")]
	public class Category
    {

		[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string CategoryId{ get; set; }

		[DataType(DataType.Text),MaxLength(50)]
        public string CategoryName { get; set; }
        public string Description {get; set;}

        [Timestamp]
        public byte[] Timestamp {get; set;}
        
	}
}