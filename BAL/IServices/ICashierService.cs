using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;
using Model.Entities;

namespace BAL.IServices
{
    public interface ICashierService
    {
        Task AddToCart(AddToCartDTO inputModel);
        Task<IEnumerable<Cart>> GetCart();
        Task FinalizeCart();
    }
}
