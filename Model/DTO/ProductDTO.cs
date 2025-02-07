using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class ProductDTO
    {
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public decimal ProfitPerItem { get; set; }
    }

    public class DeleteProductDTO
    {
        public Guid Id { get; set; }
    }

    public class UpdateProductDTO
    {
        public Guid Id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public decimal ProfitPerItem { get; set; }
    }

    public class AddToCartDTO
    {
        public Guid id { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; } 
        public int Quantity { get; set; }
    }
}
