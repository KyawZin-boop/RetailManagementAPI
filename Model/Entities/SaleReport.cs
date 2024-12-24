﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.AppConfig;

namespace Model.Entities
{
    [Table("Tbl_SaleReport")]
    public class SaleReport : Common
    {
        [Key]
        public Guid SaleId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Total { get; set; }
    }
}