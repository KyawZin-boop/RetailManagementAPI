using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.AppConfig;
using Model.DTO;
using Model.Entities;

namespace BAL.IServices
{
    public interface IProductService
    {
        Task AddProduct(ProductDTO inputModel);
        Task DeleteProduct(Guid id);
        Task UpdateProduct(UpdateProductDTO inputModel);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(Guid id);
        Task<PaginatedResponseModel<Product>> GetByConditionWithPaginationByDesc(int page, int pageSize);

    }
}
