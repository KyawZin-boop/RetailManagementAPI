using BAL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.AppConfig;
using Model.DTO;
using Model.Entities;
using Repository.UnitOfWork;

namespace RetailAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;

        public ProductController(IUnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var AllProduct = await _productService.GetAllProducts();
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = AllProduct });
            }catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpGet("GetByConditionWithPaginationByDesc")]
        public async Task<IActionResult> GetByConditionWithPaginationByDesc(int page, int pageSize)
        {
            try
            {
                var products = await _productService.GetByConditionWithPaginationByDesc(page, pageSize);
                return Ok(new ResponseModel { Message = "Success.", Status = APIStatus.Successful, Data = products });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = product });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO inputModel)
        {
            try
            {
                await _productService.AddProduct(inputModel);
                return Ok(new ResponseModel { Message = "Successfully Added.", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return Ok(new ResponseModel { Message = "Successfully Deleted.", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO inputModel)
        {
            try
            {
                await _productService.UpdateProduct(inputModel);
                return Ok(new ResponseModel { Message = "Successfully Updated.", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpGet("GetAllProductWithPagination")]
        public async Task<IActionResult> GetAllProductWithPagination(int page, int pageSize)
        {
            try
            {
                var data = await _productService.GetByConditionWithPaginationByDesc(page, pageSize);
                return Ok(new ResponseModel { Message = "Successfully Updated.", Status = APIStatus.Successful, Data = data });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }
    }
}
