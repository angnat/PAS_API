using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PAS_API.Model;
using PAS_API.Model.DTO;
using PAS_API.Repository.IRepository;

namespace PAS_API.Controller
{
    [Route("api/AdminUnitPengalihanListHutangAPI")]
    [ApiController]
    public class AdminUnitPengalihanListHutangController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IAdminUnitPengalihanListHutangRepository _db_T_AdminUnitListHutang;
        private readonly IMapper _mapper;
        public AdminUnitPengalihanListHutangController(IAdminUnitPengalihanListHutangRepository db_T_AdminUnitListHutang, IMapper mapper)
        {
            _db_T_AdminUnitListHutang = db_T_AdminUnitListHutang;
            _mapper = mapper;
            this._response = new();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> InsertListHutangPAS([FromBody] AdminUnitPengalihanListHutangDTO[] createDTO)
        {
            try
            {
                if (createDTO == null) return BadRequest();
                for (int i = 0; i < createDTO.Length; i++)
                {
                    var existingProgress = await _db_T_AdminUnitListHutang.GetAsync(u => u.AdminUnitID == createDTO[i].AdminUnitID && u.FIDPengalihan == createDTO[i].FIDPengalihan && u.TypeHutangID == createDTO[i].TypeHutangID);
                    if (existingProgress != null)
                    {
                        // If the progress with the same UnitID exists, update the existing progress
                        _mapper.Map(createDTO[i], existingProgress); // Update the existing progress with the new values
                        await _db_T_AdminUnitListHutang.UpdateAsync(existingProgress);                       
                    }
                    else
                    {
                        AdminUnitPengalihanListHutang listhutang = _mapper.Map<AdminUnitPengalihanListHutang>(createDTO[i]);
                        await _db_T_AdminUnitListHutang.CreateAsync(listhutang);                     
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