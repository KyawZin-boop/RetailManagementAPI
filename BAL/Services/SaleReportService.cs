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

        public async Task AddSaleReport(SaleReportDTO inputModel)
        {
            try
            {
                var saleReport = new SaleReport
                {
                    ProductCode = inputModel.ProductCode,
                    ProductName = inputModel.ProductName,
                    Quantity = inputModel.Quantity,
                    SellingPrice = inputModel.SellingPrice,
                    Total = inputModel.Total
                };
                await _unitOfWork.SaleReport.Add(saleReport);
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
