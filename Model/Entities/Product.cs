using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.AppConfig;

namespace Model.Entities
{
    [Table("Tbl_Product")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public decimal ProfitPerItem { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime? UpdatedDate { get; set; }
        public bool ActiveFlag { get; set; } = true;
    }
}
