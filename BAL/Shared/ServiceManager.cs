using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.IServices;
using BAL.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Model;
using Model.AppConfig;
using Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.IRepositories;

namespace BAL.Shared
{
    public class ServiceManager
    {
        public static void SetServiceInfo(IServiceCollection services, AppSettings appSettings)
        {
            services.AddDbContextPool<DataContext>(options =>
            {
                options.UseSqlServer(appSettings.connectionString);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleReportService, SaleReportService>();
        }
    }
}
