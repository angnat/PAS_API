using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PAS_API.Model;
using PAS_API.Model.DTO;
using PAS_API.Repository.IRepository;


namespace PAS_API.Controller
{
    [Route("api/ProgressAPI")]
    [ApiController]
    public class ProgressAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IProgressRepository _db_Progress;     
        private readonly IMapper _mapper;
        public ProgressAPIController(IProgressRepository db_Progress, IMapper mapper)
        {
            _db_Progress = db_Progress;
            _mapper = mapper;
            this._response = new();
           // _dbUnit = dbUnit;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetProgress([FromQuery] string? UnitID)
        {
            try
            {
                if (string.IsNullOrEmpty(UnitID))
                {
                    //_logger.Log("Getting Villa error with ID " + id,"error");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
               

                var m_progress = await _db_Progress.GetAsync(u => u.UnitID == UnitID, true, order: u => u.ID);

                _response.Result = _mapper.Map<ProgressDTO>(m_progress);
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
