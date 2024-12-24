using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Entities;
using Repository.Repositories.IRepositories;

namespace Repository.Repositories.Repositories
{
    internal class SaleReportRepository : GenericRepository<SaleReport> , ISaleReportRepository
    {
      public SaleReportRepository(DataContext context) : base(context)
        {
        }
    }
}
