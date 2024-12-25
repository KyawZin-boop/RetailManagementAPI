using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.IServices;
using Model;
using Model.DTO;
using Model.Entities;
using Repository.UnitOfWork;

namespace BAL.Services
{
    internal class SaleReportService : ISaleReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task AddSaleReport(Product product, int Quantity)
        //{
        //    try
        //    {
        //        var saleReport = new SaleReport
        //        {
        //            ProductCode = product.ProductCode,
        //            ProductName = product.Name,
        //            Quantity = Quantity,
        //            SellingPrice = product.Price,
        //            TotalPrice = product.Price * Quantity,
        //            Profit = product.ProfitPerItem * Quantity
        //        };
        //        await _unitOfWork.SaleReport.Add(saleReport);
        //        await _unitOfWork.SaveChangesAsync();
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task GetSaleReport()
        {
            try
            {
                var saleReports = await _unitOfWork.SaleReport.GetAll();
                if (saleReports is null)
                {
                    throw new Exception("No sale reports found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task GetSaleReportById(Guid id)
        {
            try
            {
                var saleReports = await _unitOfWork.SaleReport.GetByCondition(x => x.SaleId == id);
                if (saleReports is null)
                {
                    throw new Exception("No sale reports found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
