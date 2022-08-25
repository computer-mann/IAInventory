using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace WebApi.Models.Store
{
    public class Tags
    {
    	[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id {get;set;}
		
		[MaxLength(1000)]
		public string ProductTags {set; get;}

		[Timestamp]
        public byte[] Timestamp {get; set;}
    }
}