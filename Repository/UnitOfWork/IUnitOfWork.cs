using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories.IRepositories;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        ISaleReportRepository SaleReport { get; }
        ICartRepository Cart { get; }
        IUserRepository User { get; }
        Task<int> SaveChangesAsync();
    }
}
