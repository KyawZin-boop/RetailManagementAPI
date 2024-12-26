﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;
using Model.Entities;

namespace BAL.IServices
{
    public interface IProductService
    {
        Task AddProduct(ProductDTO inputModel);
        Task DeleteProduct(DeleteProductDTO inputModel);
        Task UpdateProduct(UpdateProductDTO inputModel);
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<Product> GetProductById(Guid id);
        
    }
}
