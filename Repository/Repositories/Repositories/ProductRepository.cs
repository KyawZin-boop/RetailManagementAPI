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
    internal class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }
    }
}
