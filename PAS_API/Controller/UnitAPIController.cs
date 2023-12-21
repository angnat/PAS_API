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

        [HttpGet("{UnitID}", Name = "GetUnit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetUnit(string UnitID)
        {
            try
            {
                if (string.IsNullOrEmpty(UnitID))
                {
                    //_logger.Log("Getting Villa error with ID " + id,"error");
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                //var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
                var unit = await _dbUnit.GetAsync(u => u.UnitID == UnitID);
                if (unit == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<UnitDTO>(unit);
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
        public async Task<ActionResult<APIResponse>> InsertZMUNIT([FromBody] CreateUnitDTO[] createDTO)
        {
            try
            {
                for (int i = 0; i < createDTO.Length; i++)
                {
                    var existingUnit = await _dbUnit.GetAsync(u => u.UnitID.ToLower() == createDTO[i].UnitID.ToLower());
                    if (existingUnit != null)
                    {
                        // If the progress with the same UnitID exists, update the existing progress
                        // Modify the FIDCluster in the existingUnit object                  
                        _mapper.Map(createDTO[i], existingUnit); // Update the existing progress with the new values
                        existingUnit.FIDCluster = 51; // Update the FIDCluster value
                        await _dbUnit.UpdateAsync(existingUnit);
                        //_response.Result = _mapper.Map<ProgressDTO>(existingProgress);
                    }
                    else
                    {
                        if (createDTO == null) return BadRequest(createDTO[i]);
                        Unit unit = _mapper.Map<Unit>(createDTO[i]);
                        await _dbUnit.CreateAsync(unit);
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

        //public int FindCluster(string ProjectCode)
        //{
        //    List<Project> proj = new ProjectBL().Select(string.Format("{0} like '%{1}%'", Project.Columns.PROJECTCODE, project), Project.Columns.PROJECTCODE);
        //    if (proj.Count == 0)
        //    {
        //        ("return " + project + "not found. Create Project").AppendLog(m_lstLog);
        //        Project projectItem = new Project();
        //        projectItem.FIDDivision = intDiv;
        //        projectItem.ProjectCode = BA_code;
        //        projectItem.ProjectName = BA_desc;
        //        result = new ProjectBL().Insert(projectItem);

        //        // The rest of the logic for inserting a Cluster goes here
        //    }
        //    else
        //    {
        //        // The logic for handling an existing project goes here
        //    }

        //    return 0;
        //}
    }
}
