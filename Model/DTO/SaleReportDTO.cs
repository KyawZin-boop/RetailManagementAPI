﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class SaleReportDTO
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Total { get; set; }
    }

    public class ReportDateDTO
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }

    public class TotalSaleProduct
    {
        public string ProductName { get; set; }
        public int SaleCount { get; set; }
    }
}
