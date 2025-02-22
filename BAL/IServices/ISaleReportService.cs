﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.AppConfig;
using Model.DTO;
using Model.Entities;

namespace BAL.IServices
{
    public interface ISaleReportService
    {
        //Task AddSaleReport(SaleReportDTO inputModel);
        Task<IEnumerable<SaleReport>> GetSaleReport();
        Task<IEnumerable<SaleReport>> GetSaleReportByDate(DateTime date);
        Task<IEnumerable<SaleReport>> GetSaleReportWithinRange(ReportDateDTO date);
        Task<Summary> GetTotalSummary();
        Task<Summary> GetTotalSummaryByDay(DateTime date);
        Task<PaginatedResponseModel<SaleReport>> GetPaginationByDesc(int page, int pageSize);
        Task<PaginatedResponseModel<SaleReport>> GetSaleReportBySearch(DateTime date);
        Task<IEnumerable<TotalSaleProduct>> GetTotalSaleCountForEachProduct();
        Task<IEnumerable<TotalSaleProduct>> GetTotalSaleCountByDay(DateTime date);
    }
}
