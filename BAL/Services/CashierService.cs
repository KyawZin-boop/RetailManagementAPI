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

        public CashierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddToCart(IEnumerable<AddToCartDTO> inputModel)
        {
            try
            {
                foreach (var item in inputModel)
                {
                    if (item.Quantity <= 0)
                    {
                        throw new Exception("Quantity must be greater than 0");
                    }

                    var product = (await _unitOfWork.Product.GetByCondition(x => x.ProductCode == item.ProductCode && x.ActiveFlag)).FirstOrDefault();
                    if (product is null)
                    {
                        throw new Exception("Product not found");
                    }

                    if(product.Stock < item.Quantity)
                    {
                        throw new Exception("Not enough stock");
                    }
                    product.Stock -= item.Quantity;
                    _unitOfWork.Product.Update(product);

                    await _unitOfWork.SaleReport.Add(new SaleReport
                    {
                        ProductCode = product.ProductCode,
                        ProductName = product.Name,
                        Quantity = item.Quantity,
                        SellingPrice = product.Price,
                        ProfitPerItem = product.ProfitPerItem,
                        TotalPrice = product.Price * item.Quantity,
                        TotalProfit = product.ProfitPerItem * item.Quantity,
                        SaleDate = DateTime.UtcNow
                    });
                }
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
