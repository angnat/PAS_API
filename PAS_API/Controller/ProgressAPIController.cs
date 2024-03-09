using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PAS_API.Logging;
using PAS_API.Model;
using PAS_API.Model.DTO;
using PAS_API.Repository.IRepository;
using Serilog;
using System.Net;
using System.Text.Json;

namespace PAS_API.Controller
{
    [Route("api/ProgressAPI")]
    [ApiController]
    public class ProgressAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IProgressRepository _db_Progress;     
        private readonly IMapper _mapper;
        private readonly ILogging _logger;
        public ProgressAPIController(IProgressRepository db_Progress, IMapper mapper, ILogging logger)
        {
            _db_Progress = db_Progress;
            _mapper = mapper;
            this._response = new();
            _logger = logger;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> InsertZMPROG([FromBody] CreateProgressDTO[] createDTO)
        {
            try
            {
                if (createDTO == null) return BadRequest();
                string ip = Response.HttpContext.Connection.RemoteIpAddress.ToString();
                if(ip == "::1")
                {
                    ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
                }
               
                for (int i = 0; i < createDTO.Length; i++)
                {
                    string strJson = JsonSerializer.Serialize<CreateProgressDTO>(createDTO[i]);
                    var existingProgress = await _db_Progress.GetAsync(u => u.UnitID.ToLower() == createDTO[i].UnitID.ToLower());
                    if (existingProgress != null)
                    {
                        // If the progress with the same UnitID exists, update the existing progress                      
                        createDTO[i].ModifiedHost = ip;
                        _logger.Log("UPDATE","INFORMATION");                                          
                        _logger.Log(strJson.ToString(), "INFORMATION");
                        Log.Information("Parameter: " + strJson);
                        _mapper.Map(createDTO[i], existingProgress); // Update the existing progress with the new values
                        await _db_Progress.UpdateAsync(existingProgress);                               
                    }
                    else
                    {
                        createDTO[i].CreatedHost = ip;
                        Progress progress = _mapper.Map<Progress>(createDTO[i]);
                        await _db_Progress.CreateAsync(progress);                     
                    }
                }
               
                _response.StatusCode = System.Net.HttpStatusCode.Created;
                _response.IsSuccess = true;
                Log.Information("Status: " + _response);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessage = new List<string>() { ex.ToString() };
                Log.Error("Error: " + _response);
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{string}", Name = "UpdateProgress")]       
        public async Task<ActionResult<APIResponse>> UpdateProgress(string unitid, [FromBody] CreateProgressDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || unitid != updateDTO.UnitID)
                {
                    return BadRequest();
                }
                Progress progress = _mapper.Map<Progress>(updateDTO);
                // var progress = await _db_Progress.GetAsync(u => u.UnitID == unitid);

                _db_Progress.UpdateAsync(progress);
                _response.Result = _mapper.Map<CreateProgressDTO>(progress);
                _response.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorsMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }       

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{string}", Name = "UpdatePartialProgress")]
        public async Task<IActionResult> UpdatePartialProgress(string unitid, JsonPatchDocument<CreateProgressDTO> patchDTO)
        {
            if (patchDTO == null)
            {
                return BadRequest();
            }
            //var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            var progress = await _db_Progress.GetAsync(u => u.UnitID == unitid);
            if (progress == null)
            {
                return BadRequest();
            }
            CreateProgressDTO progressDTO = _mapper.Map<CreateProgressDTO>(progress);

            patchDTO.ApplyTo(progressDTO, ModelState);
            Progress model = _mapper.Map<Progress>(progressDTO);

            _db_Progress.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                return NoContent();
            }

        }

    }
}
