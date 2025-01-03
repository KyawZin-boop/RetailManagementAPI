﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.IServices;
using Model.DTO;
using Model.Entities;
using Repository.UnitOfWork;

namespace BAL.Services
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            try
            {
                var products = await _unitOfWork.Product.GetByCondition(x => x.ActiveFlag);
                
                if (products is null)
                {
                    throw new Exception("No products found");
                }
                var resproducts = _mapper.Map<IEnumerable<ProductDTO>>(products);

                return resproducts;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<Product> GetProductById(Guid id)
        {
            try
            {
                var product = (await _unitOfWork.Product.GetByCondition(x => x.Id == id)).FirstOrDefault();
                if (product is null)
                {
                    throw new Exception("Product not found");
                }

                return product;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddProduct(ProductDTO inputModel)
        {
            try
            {
                var product = new Product
                {
                    ProductCode = inputModel.ProductCode,
                    Name = inputModel.Name,
                    Price = inputModel.Price,
                    Stock = inputModel.Stock,
                    ProfitPerItem = inputModel.ProfitPerItem
                };
                await _unitOfWork.Product.Add(product);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task DeleteProduct(DeleteProductDTO inputModel)
        {
            try
            {
                var product = await _unitOfWork.Product.GetByGuid(inputModel.Id);
                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                product.ActiveFlag = false;
                _unitOfWork.Product.Update(product);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateProduct(UpdateProductDTO inputModel)
        {
            try
            {
                var product = (await _unitOfWork.Product.GetByCondition(x => x.Id == inputModel.Id && x.ActiveFlag)).FirstOrDefault();
                if (product == null)
                {
                    throw new Exception("Product not found");
                }

                product.ProductCode = inputModel.ProductCode;
                product.Name = inputModel.Name;
                product.Price = inputModel.Price;
                product.Stock = inputModel.Stock;
                product.ProfitPerItem = inputModel.ProfitPerItem;

                _unitOfWork.Product.Update(product);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
