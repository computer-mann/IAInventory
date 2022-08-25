using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace WebApi.Models.Store{

    [Table(name: "Brand")]
	public class CatalogBrand
        {
        	[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        	public string BrandId {get; set;}

        	[DataType(DataType.Text),MaxLength(50)]
        	public string BrandName { get; set;}

            [DataType(DataType.Text)]
            public string Description { get; set;}

            [DataType(DataType.Text)]
            public string BrandImage { get; set;}

        	[Timestamp]
        	public byte[] Timestamp {get; set;}
        }
    }