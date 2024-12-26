using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;
using Model.Entities;

namespace BAL.IServices
{
    public interface ISaleReportService
    {
        //Task AddSaleReport(SaleReportDTO inputModel);
        Task<IEnumerable<SaleReport>> GetSaleReport();
        Task<SaleReport> GetSaleReportById(Guid id);
        Task<Summary> GetTotalSummary();
        Task<Summary> GetTotalSummaryByDay(DateTime date);
    }
}
