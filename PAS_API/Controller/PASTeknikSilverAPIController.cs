using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PAS_API.Model;
using PAS_API.Model.DTO;
using PAS_API.Repository.IRepository;


namespace PAS_API.Controller
{
    [Route("api/PASTeknikSilver")]
    [ApiController]
    public class PASTeknikSilverAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IAdminUnitTeknikSilverRepository _db_Silver;
        private readonly IMapper _mapper;
        public PASTeknikSilverAPIController(IAdminUnitTeknikSilverRepository db_Silver, IMapper mapper)
        {
            _db_Silver = db_Silver;
            _mapper = mapper;
            this._response = new();          
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> InsertSilverToPAS([FromBody] AdminUnitTeknikSilverDTO[] createDTO)
        {
            try
            {
                if (createDTO == null) return BadRequest();
                for (int i = 0; i < createDTO.Length; i++)
                {
                    var existingProgress = await _db_Silver.GetAsync(u => u.UnitId.ToLower() == createDTO[i].UnitId.ToLower());
                    if (existingProgress != null)
                    {
                        // If the progress with the same UnitID exists, update the existing progress
                        _mapper.Map(createDTO[i], existingProgress); // Update the existing progress with the new values
                        await _db_Silver.UpdateAsync(existingProgress);
                        //_response.Result = _mapper.Map<ProgressDTO>(existingProgress);
                    }
                    else
                    {
                        AdminUnitTeknikSilver silver = _mapper.Map<AdminUnitTeknikSilver>(createDTO[i]);
                        await _db_Silver.CreateAsync(silver);
                        //_response.Result = _mapper.Map<ProgressDTO>(progress);
                    }
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
