using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;
using Model.Entities;

namespace BAL.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(Guid id);
        Task<string> CreateUser(UserDTO inputModel);
        Task UpdateUser(UserUpdateDTO inputModel);
        Task DeleteUser(Guid id);
        Task<string> LoginUser(UserLoginDTO inputModel);
    }
}
