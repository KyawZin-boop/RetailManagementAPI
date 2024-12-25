using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.IServices;
using Model.DTO;
using Model.Entities;
using Repository.UnitOfWork;

namespace BAL.Services
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<Cart> carts = new List<Cart>();

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                var products = await _unitOfWork.Product.GetByCondition(x => x.ActiveFlag);
                if (products is null)
                {
                    throw new Exception("No products found");
                }

                return products;
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

        public async Task AddToCart(AddToCartDTO inputModel)
        {
            try
            {
                var product = (await _unitOfWork.Product.GetByCondition(x => x.ProductCode == inputModel.ProductCode && x.ActiveFlag)).FirstOrDefault();
                if (product is null)
                {
                    throw new Exception("Product not found");
                }

                var cart = new Cart
                {
                    ProductCode = inputModel.ProductCode,
                    Quantity = inputModel.Quantity
                };
                carts.Add(cart);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Cart>> GetCart()
        {
            try
            {
                var lst = await _unitOfWork.Cart.GetAll();
                if (lst is null)
                {
                    throw new Exception("No item in Cart");
                }

                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task FinalizeCart()
        {
            try
            {
                foreach (var cart in carts)
                {
                    var product = (await _unitOfWork.Product.GetByCondition(x => x.ProductCode == cart.ProductCode && x.ActiveFlag)).FirstOrDefault();
                    if (product is null)
                    {
                        throw new Exception("Product not found");
                    }
                    product.Stock -= cart.Quantity;
                    _unitOfWork.Product.Update(product);
                    var report = new SaleReport
                    {
                        ProductCode = product.ProductCode,
                        ProductName = product.Name,
                        Quantity = cart.Quantity,
                        SellingPrice = product.Price,
                        TotalPrice = product.Price * cart.Quantity,
                        Profit = product.ProfitPerItem * cart.Quantity
                    };
                     await _unitOfWork.SaleReport.Add(report);
                }
                carts.Clear();
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
