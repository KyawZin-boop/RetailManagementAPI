using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;

namespace BAL.IServices
{
    public interface ISaleReportService
    {
        //Task AddSaleReport(SaleReportDTO inputModel);
        Task GetSaleReport();
        Task GetSaleReportById(Guid id);
    }
}
