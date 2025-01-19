using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Cart
    {
        public string ProductCode { get; set; }
        [Column("ProductName")]
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
