﻿using AutoMapper;
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

        [HttpPost]    
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> InsertZMPROG([FromBody] CreateProgressDTO createDTO)
        {
            try
            {
                var existingProgress = await _db_Progress.GetAsync(u => u.UnitID.ToLower() == createDTO.UnitID.ToLower());
                if (existingProgress != null)
                {
                    // If the progress with the same UnitID exists, update the existing progress
                    return await UpdateProgress(existingProgress.UnitID, createDTO);
                }
                if (createDTO == null) return BadRequest(createDTO);
               
                Progress progress = _mapper.Map<Progress>(createDTO);
          
                await _db_Progress.CreateAsync(progress);
                _response.Result = _mapper.Map<ProgressDTO>(progress);
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

    }
}
