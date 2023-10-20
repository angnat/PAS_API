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
        private readonly ITUnitRepository _dbUnit;
        private readonly IMapper _mapper;
        public TUnitAPIController(ITUnitRepository dbUnit, IMapper mapper)
        {
            _dbUnit = dbUnit;
            _mapper = mapper;
            this._response = new();
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

                //var unit = await _dbUnit.GetAsync(u => u.MaterialIDSAP == UnitID);
                var unit = await _dbUnit.GetAsync(u => u.MaterialIDSAP == UnitID, true , order: x => x.ID);
               // var orderedUnit = unit.OrderByDescending(u => u.PropertyToOrderBy).ToList();

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
