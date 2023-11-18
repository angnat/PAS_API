using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PAS_API.Model;
using PAS_API.Model.DTO;
using PAS_API.Repository.IRepository;

namespace PAS_API.Controller
{
    [Route("api/AdminUnitTeknikAPI")]
    [ApiController]
    public class AdminUnitTeknikAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IAdminUnitTeknikRepository _db_T_AdminUnitTeknik;
        private readonly IUnitRepository _dbUnit;
        private readonly IMapper _mapper;
        public AdminUnitTeknikAPIController(IAdminUnitTeknikRepository db_T_AdminUnitTeknik, IMapper mapper, IUnitRepository dbUnit)
        {
            _db_T_AdminUnitTeknik = db_T_AdminUnitTeknik;
            _mapper = mapper;
            this._response = new();
            _dbUnit = dbUnit;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetTAdminUnitTeknik([FromQuery] string? UnitID)
        {
            try
            {
                if (string.IsNullOrEmpty(UnitID))
                {
                    //_logger.Log("Getting Villa error with ID " + id,"error");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var unit = await _dbUnit.GetAsync(u => u.UnitID == UnitID, true, order: u => u.ID);

                if (unit == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                var t_unit_teknik = await _db_T_AdminUnitTeknik.GetAsync(u => u.FIDAdminUnit == unit.ID, true, order: u => u.ID,includeProperties: "Unit");

                _response.Result = _mapper.Map<AdminUnitTeknikDTO>(t_unit_teknik);
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
