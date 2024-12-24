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
    internal class CartRepository : GenericRepository<Cart> , ICartRepository
    {
        public CartRepository(DataContext context) : base(context) { }
    }
}
