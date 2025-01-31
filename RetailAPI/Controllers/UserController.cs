using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.AppConfig;
using Model.DTO;

namespace RetailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsers();
                return Ok(new ResponseModel { Message = "Success.", Status = APIStatus.Successful, Data = users });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [Authorize]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                return Ok(new ResponseModel { Message = "Success.", Status = APIStatus.Successful, Data = user });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDTO inputModel)
        {
            try
            {
                var res = await _userService.CreateUser(inputModel);
                return Ok(new ResponseModel { Message = "Success.", Status = APIStatus.Successful, Data = res });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [Authorize]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserUpdateDTO inputModel)
        {
            try
            {
                await _userService.UpdateUser(inputModel);
                return Ok(new ResponseModel { Message = "Success.", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [Authorize]
        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok(new ResponseModel { Message = "Success.", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserLoginDTO inputModel)
        {
            try
            {
                var res = await _userService.LoginUser(inputModel);
                return Ok(new ResponseModel { Message = "Login Success.", Status = APIStatus.Successful, Data = res });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
}
