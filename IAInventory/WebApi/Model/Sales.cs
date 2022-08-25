using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SuitStore.Models.Store;

namespace SuitStore.Models.Store
{
    public class Sales
    {
		public string Id {get; set;}
		public int SaleNo {get; set;}
		[Timestamp]
        public byte[] Timestamp {get; set;}
    }
}