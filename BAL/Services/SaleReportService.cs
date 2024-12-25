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
    internal class SaleReportService :  ISaleReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Summary Summary = new Summary();

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

        public async Task<IEnumerable<SaleReport>> GetSaleReport()
        {
            try
            {
                var saleReports = await _unitOfWork.SaleReport.GetAll();
                if (saleReports is null)
                {
                    throw new Exception("No sale reports found");
                }

                return saleReports;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SaleReport> GetSaleReportById(Guid id)
        {
            try
            {
                var saleReport = (await _unitOfWork.SaleReport.GetByCondition(x => x.SaleId == id)).FirstOrDefault();
                if (saleReport is null)
                {
                    throw new Exception("No sale reports found");
                }

                return saleReport;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Summary> GetTotalSummary()
        {
            try
            {
                var saleReports = await _unitOfWork.SaleReport.GetAll();
                if(saleReports is null)
                {
                    throw new Exception("No sale reports found");
                }

                foreach (var report in saleReports)
                {
                    Summary.TotalProfit += report.Profit;
                    Summary.TotalRevenue += report.TotalPrice;
                }
                return Summary;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
