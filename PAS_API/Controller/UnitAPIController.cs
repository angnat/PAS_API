using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAS_API.Model;
using PAS_API.Model.DTO;
using PAS_API.Repository.IRepository;

namespace PAS_API.Controller
{
    [Route("api/UnitAPI")]
    [ApiController]
    public class UnitAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitRepository _dbUnit;
        private readonly IMapper _mapper;

        public UnitAPIController(IUnitRepository dbUnit, IMapper mapper)
        {
            _dbUnit = dbUnit;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetUnit()
        {     
            try
            {
                IEnumerable<Unit> unitList = await _dbUnit.GetAllAsync();
                _response.Result = _mapper.Map<List<UnitDTO>>(unitList);
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
