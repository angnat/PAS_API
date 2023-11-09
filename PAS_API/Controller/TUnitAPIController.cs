using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PAS_API.Model;
using PAS_API.Model.DTO;
using PAS_API.Repository.IRepository;

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
        public TUnitAPIController(ITUnitRepository db_T_Unit, IMapper mapper,IUnitRepository dbUnit)
        {
            _db_T_Unit = db_T_Unit;
            _mapper = mapper;
            this._response = new();
            _dbUnit = dbUnit;
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
             
                var unit = await _db_T_Unit.GetAsync(u => u.MaterialIDSAP == UnitID, true , order: u => u.ID, includeProperties : "Unit");
             
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

    }
}
