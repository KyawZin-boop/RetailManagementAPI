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

        public async Task<IEnumerable<SaleReport>> GetSaleReportByDate(DateTime date)
        {
            try
            {
                DateTime utcStart = date.Date.ToUniversalTime(); // Start of the day in UTC
                DateTime utcEnd = date.Date.AddDays(1).AddTicks(-1).ToUniversalTime();

                var saleReports = await _unitOfWork.SaleReport.GetByCondition(x => x.SaleDate >= utcStart && x.SaleDate <= utcEnd);
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
                    Summary.TotalProfit += report.TotalProfit;
                    Summary.TotalRevenue += report.TotalPrice;
                }
                return Summary;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Summary> GetTotalSummaryByDay(DateTime date)
        {
            try
            {
                DateTime utcStart = date.Date.ToUniversalTime(); // Start of the day in UTC
                DateTime utcEnd = date.Date.AddDays(1).AddTicks(-1).ToUniversalTime();

                var saleReports = await _unitOfWork.SaleReport.GetByCondition(x => x.SaleDate >= utcStart && x.SaleDate <= utcEnd);
                if (saleReports is null)
                {
                    throw new Exception("No sale reports found");
                }
                foreach (var report in saleReports)
                {
                    Summary.TotalProfit += report.TotalProfit;
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
