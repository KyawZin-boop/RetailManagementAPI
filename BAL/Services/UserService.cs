using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Common;
using BAL.IServices;
using Model.DTO;
using Model.Entities;
using Repository.UnitOfWork;

namespace BAL.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly TokenProvider _tokenProvider;

    public UserService(IUnitOfWork unitOfWork, IMapper imapper, TokenProvider tokenProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = imapper;
        _tokenProvider = tokenProvider;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        try
        {
            var users = await _unitOfWork.User.GetByCondition(x => x.ActiveFlag);
            //var userDto = _mapper.Map<IEnumerable<UserDTO>>(users);
            return users;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<User> GetUserById(Guid id)
    {
        try
        {
            var user = (await _unitOfWork.User.GetByCondition(x => x.UserId == id && x.ActiveFlag)).FirstOrDefault();
            if (user is null)
            {
                throw new Exception("User not found.");
            }
            return user;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> CreateUser(UserDTO inputModel)
    {
        try
        {
            var user = new User
            {
                Name = inputModel.Name,
                Email = inputModel.Email,
                Password = inputModel.Password,
            };
            await _unitOfWork.User.Add(user);
            await _unitOfWork.SaveChangesAsync();

            string token = _tokenProvider.Create(user);

            return token;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task UpdateUser(UserUpdateDTO inputModel)
    {
        try
        {
            var user = (await _unitOfWork.User.GetByCondition(x => x.UserId == inputModel.UserId && x.ActiveFlag)).FirstOrDefault();
            if (user is null)
            {
                throw new Exception("User not found.");
            }

            if (inputModel.Name != null)
            {
                user.Name = inputModel.Name;
            }
            if (inputModel.Email != null)
            {
                user.Email = inputModel.Email;
            }

            user.UpdatedBy = "user";
            user.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task DeleteUser(Guid id)
    {
        try
        {
            var user = (await _unitOfWork.User.GetByCondition(x => x.UserId == id)).FirstOrDefault();
            if (user is null)
            {
                throw new Exception("User not found.");
            }

            user.UpdatedAt = DateTime.UtcNow;
            user.ActiveFlag = false;
            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> LoginUser(UserLoginDTO inputModel)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(inputModel.Email) || string.IsNullOrWhiteSpace(inputModel.Password))
            {
                throw new ArgumentException("UserName and Password cannot be null or empty.");
            }

            var user = (await _unitOfWork.User.GetByCondition(x => x.Email == inputModel.Email && x.Password == inputModel.Password && x.ActiveFlag)).FirstOrDefault();
            if (user is null)
            {
                throw new Exception("Incorrect Password or UserName.");
            }

            string token = _tokenProvider.Create(user);

            return token;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
