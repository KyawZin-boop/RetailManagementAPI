using BAL.IServices;
using Microsoft.AspNetCore.Mvc;
using Model.AppConfig;
using Model.DTO;
using Repository.UnitOfWork;

namespace RetailAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AddToCartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICashierService _cashierService;

        public AddToCartController(IUnitOfWork unitOfWork, ICashierService cashierService)
        {
            _unitOfWork = unitOfWork;
            _cashierService = cashierService;
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(AddToCartDTO inputModel)
        {
            try
            {
                await _cashierService.AddToCart(inputModel);
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var cart = await _cashierService.GetCart();
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = cart });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("FinalizeCart")]
        public async Task<IActionResult> FinalizeCart()
        {
            try
            {
                await _cashierService.FinalizeCart();
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
}
