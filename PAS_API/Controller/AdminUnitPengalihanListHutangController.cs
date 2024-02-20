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
        private readonly IAdminUnitPengalihanRepository _dbPengalihan;
        private readonly IMapper _mapper;
        public AdminUnitPengalihanListHutangController(IAdminUnitPengalihanListHutangRepository db_T_AdminUnitListHutang, IMapper mapper, IAdminUnitPengalihanRepository dbPengalihan)
        {
            _db_T_AdminUnitListHutang = db_T_AdminUnitListHutang;
            _mapper = mapper;
            this._response = new();
            _dbPengalihan = dbPengalihan;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> InsertListHutangPAS([FromBody] CreateAdminUnitPengalihanListHutangDTO[] createDTO)
        {
            try
            {
                if (createDTO == null) return BadRequest();
                for (int i = 0; i < createDTO.Length; i++)
                {
                    var pengalihan = await _dbPengalihan.GetAsync(u => u.RequestNumber == createDTO[i].RequestNumber);
                    if (pengalihan != null)
                    {
                        var existingProgress = await _db_T_AdminUnitListHutang.GetAsync(u => u.AdminUnitID == pengalihan.FIDAdminUnit && u.FIDPengalihan == pengalihan.ID && u.TypeHutangID == createDTO[i].TypeHutangID);
                        if (existingProgress != null)
                        {
                            // If the progress with the same UnitID exists, update the existing progress
                            createDTO[i].FIDPengalihan = pengalihan.ID;
                            createDTO[i].AdminUnitID = pengalihan.FIDAdminUnit;
                            _mapper.Map(createDTO[i], existingProgress); // Update the existing progress with the new values
                            await _db_T_AdminUnitListHutang.UpdateAsync(existingProgress);
                        }
                        else
                        {
                            var newListHutangPengalihan = new AdminUnitPengalihanListHutang
                            {
                                FIDPengalihan = pengalihan.ID,
                                AdminUnitID = pengalihan.FIDAdminUnit,
                                TypeHutangID = createDTO[i].TypeHutangID,
                                Jumlah = createDTO[i].Jumlah,
                                ReffNumber = createDTO[i].ReffNumber,
                                JumlahBayar = createDTO[i].JumlahBayar,
                                Keterangan = createDTO[i].Keterangan,
                                TglBayar = createDTO[i].TglBayar,
                                CreatedBy = createDTO[i].CreatedBy,
                                CreatedDate = createDTO[i].CreatedDate,
                                CreatedHost = createDTO[i].CreatedHost,
                                ModifiedBy = createDTO[i].ModifiedBy,
                                ModifiedDate = createDTO[i].ModifiedDate,
                                ModifiedHost = createDTO[i].ModifiedHost
                            };

                            AdminUnitPengalihanListHutang listhutang = _mapper.Map<AdminUnitPengalihanListHutang>(newListHutangPengalihan);
                            await _db_T_AdminUnitListHutang.CreateAsync(listhutang);
                        }
                    }
                    else
                    {
                        _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                        _response.IsSuccess = false;
                        _response.ErrorsMessage = new List<string>() { "Pengalihan Not Found" };

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