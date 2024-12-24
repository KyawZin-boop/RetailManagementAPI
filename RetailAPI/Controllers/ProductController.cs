﻿using BAL.IServices;
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
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpPost("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(DeleteProductDTO inputModel)
        {
            try
            {
                await _productService.DeleteProduct(inputModel);
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful });
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
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(AddToCartDTO inputModel)
        {
            try
            {
                await _productService.AddToCart(inputModel);
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var cart = await _productService.GetCart();
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = cart });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
    
}
