using BAL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Model.AppConfig;
using Model.DTO;
using Repository.UnitOfWork;

namespace RetailAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleReportService _saleReportSerice;

        public ManagerController(IUnitOfWork unitOfWork, ISaleReportService saleReportService)
        {
            _unitOfWork = unitOfWork;
            _saleReportSerice = saleReportService;
        }

        [HttpGet("GetSaleReport")]
        public async Task<IActionResult> GetSaleReport()
        {
            try
            {
                var reports = await _saleReportSerice.GetSaleReport();
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = reports });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpGet("GetSaleReportByDate")]
        public async Task<IActionResult> GetSaleReportByDate(DateTime date)
        {
            try
            {
                var reports = await _saleReportSerice.GetSaleReportByDate(date);
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = reports });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("GetSaleReportWithinRange")]
        public async Task<IActionResult> GetSaleReportWithinRange(ReportDateDTO date)
        {
            try
            {
                var reports = await _saleReportSerice.GetSaleReportWithinRange(date);
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = reports });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpGet("GetTotalSummary")]
        public async Task<IActionResult> GetTotalSummary()
        {
            try
            {
                var summary = await _saleReportSerice.GetTotalSummary();
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = summary });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpGet("GetTotalSummaryByDay")]
        public async Task<IActionResult> GetTotalSummaryByDay(DateTime date)
        {
            try
            {
                var summary = await _saleReportSerice.GetTotalSummaryByDay(date);
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = summary });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
}
