using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.Store;

namespace WebApi.Models
{
    [Table(name: "Till")]
    public class Till
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string TillOpenedBy { get; set; }
        [Required]
        public DateTime TillDate { get; set; }
        public List<Sale> Sales { get; set; }
        [Required]
        public string Status { get; set; } //Open,Closed
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
