using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PAS_API.Model;
using PAS_API.Model.DTO;
using PAS_API.Repository.IRepository;

namespace PAS_API.Controller
{
    [Route("api/ReportAPI")]
    [ApiController]
    public class ReportAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IVwAdminUnitReportAPIRepository _dbReport;  
        private readonly IVwReportCompanyAPIRepository _dbReportCompany;
        private readonly IMapper _mapper;

        public ReportAPIController(IVwAdminUnitReportAPIRepository dbReport,IVwReportCompanyAPIRepository dbReportCompany, IMapper mapper)
        {
            _dbReport = dbReport;
            _dbReportCompany = dbReportCompany;
            _mapper = mapper;       
            this._response = new();
        }

       
        [HttpGet("{UnitID}", Name = "GetReportBAST")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetReportBAST(string UnitID)
        {
            try
            {
                if (string.IsNullOrEmpty(UnitID))
                {
                    //_logger.Log("Getting Villa error with ID " + id,"error");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                //var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
                var unit = await _dbReport.GetAsync(u => u.UnitID == UnitID);
                if (unit == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<vw_AdminUnitReportAPIDTO>(unit);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetReportCompany(string CompanyCode, string ProjectCode)
        {
            try
            {
                if (string.IsNullOrEmpty(CompanyCode))
                {
                    //_logger.Log("Getting Villa error with ID " + id,"error");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                //var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
                var unit = await _dbReportCompany.GetAsync(u => u.CompanyCode == CompanyCode && u.ProjectCode == ProjectCode);
                if (unit == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<vw_Company_Header>(unit);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }    
}
