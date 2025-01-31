using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Model;
using Model.AppConfig;
using Model.Entities;
using Repository.Repositories.IRepositories;
using Repository.Repositories.Repositories;

namespace Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _dataContext;

        public UnitOfWork(DataContext dataContext, IOptions<AppSettings> appsettings)
        {
            _dataContext = dataContext;
            AppSettings = appsettings.Value;
            Product = new ProductRepository(dataContext);
            Cart = new CartRepository(dataContext);
            SaleReport = new SaleReportRepository(dataContext);
            User = new UserRepository(dataContext);
        }

        public IProductRepository Product { get; set; }
        public ISaleReportRepository SaleReport { get; set; }
        public ICartRepository Cart { get; set; }
        public IUserRepository User { get; set; }
        public AppSettings AppSettings { get; set; }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
    }
}
