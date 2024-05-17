using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PAS_API.Logging;
using PAS_API.Model;
using PAS_API.Model.DTO;
using PAS_API.Repository.IRepository;
using System.Net;
using System.Text.Json;

namespace PAS_API.Controller
{
    [Route("api/TUnitAPI")]
    [ApiController]
    public class TUnitAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ITUnitRepository _db_T_Unit;
        private readonly IUnitRepository _dbUnit;
        private readonly IMapper _mapper;
        private readonly ILogging _logger;
        public TUnitAPIController(ITUnitRepository db_T_Unit, IMapper mapper, IUnitRepository dbUnit, ILogging logger)
        {
            _db_T_Unit = db_T_Unit;
            _mapper = mapper;
            this._response = new();
            _dbUnit = dbUnit;
            _logger = logger;
        }


        [HttpGet("{UnitID}", Name = "GetTUnit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetTUnit(string UnitID)
        {
            try
            {
                if (string.IsNullOrEmpty(UnitID))
                {
                    //_logger.Log("Getting Villa error with ID " + id,"error");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var unit = await _db_T_Unit.GetAsync(u => u.MaterialIDSAP == UnitID, true, order: u => u.ID, includeProperties: "Unit");

                if (unit == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<TUnitDTO>(unit);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> InsertZMTRAN([FromBody] CreateTUnitDTO[] createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                string ip = Response.HttpContext.Connection.RemoteIpAddress.ToString();
                if (ip == "::1")
                {
                    ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
                }

                for (int i = 0; i < createDTO.Length; i++)
                {

                    if (createDTO == null) return BadRequest(createDTO[i]);
                    TUnit tunit = _mapper.Map<TUnit>(createDTO[i]);

                    string strJson = JsonSerializer.Serialize<CreateTUnitDTO>(createDTO[i]);
                    var existingUnit = await _dbUnit.GetAsync(u => u.UnitID.ToLower() == createDTO[i].MaterialIDSAP.ToLower());
                    if(existingUnit != null)
                    {
                        tunit.FUnit = existingUnit.ID;
                        tunit.CreatedHost = ip;                          
                        _logger.Log("UPDATE", "INFORMATION");
                        _logger.Log(strJson.ToString(), "INFORMATION");                                        
                    }

                    await _db_T_Unit.CreateAsync(tunit);               
                }

                _response.StatusCode = System.Net.HttpStatusCode.Created;
                _response.IsSuccess = true;

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
