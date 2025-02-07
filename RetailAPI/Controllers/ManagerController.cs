using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Model.AppConfig;
using Model.DTO;
using Repository.UnitOfWork;

namespace RetailAPI.Controllers
{
    [Authorize]
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

        [HttpGet("GetSaleRecordWithPagination")]
        public async Task<IActionResult> GetSaleRecordtWithPagination(int page, int pageSize)
        {
            try
            {
                var data = await _saleReportSerice.GetPaginationByDesc(page, pageSize);
                return Ok(new ResponseModel { Message = "Successfully Updated.", Status = APIStatus.Successful, Data = data });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
            }
        }

        [HttpGet("GetSaleReportBySearch")]
        public async Task<IActionResult> GetSaleReportBySearch(DateTime date)
        {
            try
            {
                var data = await _saleReportSerice.GetSaleReportBySearch(date);
                return Ok(new ResponseModel { Message = "Success.", Status = APIStatus.Successful, Data = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpGet("GetTotalSaleCountForEachProduct")]
        public async Task<IActionResult> GetTotalSaleCountForEachProduct()
        {
            try
            {
                var data = await _saleReportSerice.GetTotalSaleCountForEachProduct();
                return Ok(new ResponseModel { Message = "Success.", Status = APIStatus.Successful, Data = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpGet("GetTotalSaleCountByDay")]
        public async Task<IActionResult> GetTotalSaleCountByDay(DateTime date)
        {
            try
            {
                var data = await _saleReportSerice.GetTotalSaleCountByDay(date);
                return Ok(new ResponseModel { Message = "Success.", Status = APIStatus.Successful, Data = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }
    }
}
