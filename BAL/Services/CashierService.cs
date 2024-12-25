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
    internal class CashierService : ICashierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<Cart> carts = new List<Cart>();

        public CashierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

                var cartItem = carts.FirstOrDefault(x => x.ProductCode == inputModel.ProductCode);
                if(cartItem is null)
                {
                    var cart = new Cart
                    {
                        ProductCode = inputModel.ProductCode,
                        ProductName = inputModel.ProductName,
                        Quantity = inputModel.Quantity
                    };
                    carts.Add(cart);
                }
                else
                {
                    cartItem.Quantity += inputModel.Quantity;
                }
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
