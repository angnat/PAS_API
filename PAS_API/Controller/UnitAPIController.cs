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
        private readonly IClusterRepository _dbCluster;
        private readonly IProjectRepository _dbProject;
        private readonly IMapper _mapper;

        public UnitAPIController(IUnitRepository dbUnit, IMapper mapper, IClusterRepository dbCluster, IProjectRepository dbProject)
        {
            _dbUnit = dbUnit;
            _mapper = mapper;
            _dbCluster = dbCluster;
            _dbProject = dbProject;
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
                        //existingUnit.FIDCluster = 51; // Update the FIDCluster value

                        var existingProject = await _dbProject.GetAsync(u => u.ProjectCode.ToLower() == existingUnit.BACode);

                        if (existingProject == null)
                        {
                            // Create a new project
                            var newProject = new Project
                            {
                                FIDDivision = 530,
                                ProjectCode = existingUnit.BACode,
                                ProjectName = existingUnit.BADesc
                            };
                            await _dbProject.CreateAsync(newProject);

                            var newCluster = new Cluster
                            {
                                FIDProject = newProject.ID,
                                SLoc = existingUnit.SLoc,
                                ClusterName = existingUnit.SLocDescription
                            };

                            await _dbCluster.CreateAsync(newCluster);
                        }
                        else
                        {
                            long? projID = existingProject.ID;
                            //projectnya ada maka dia akan insert clusternya , cek dulu Clusternya   

                            var existingCluster = await _dbCluster.GetAsync(u => u.FIDProject == projID);
                            var newCluster = new Cluster
                            {
                                FIDProject = projID,
                                SLoc = existingUnit.SLoc,
                                ClusterName = existingUnit.SLocDescription
                            };

                            await _dbCluster.CreateAsync(newCluster);
                            existingUnit.FIDCluster = newCluster.ID;// Update the FIDCluster value                            
                        }

                        await _dbUnit.UpdateAsync(existingUnit);
                    }
                    else
                    {
                        if (createDTO == null) return BadRequest(createDTO[i]);
                        Unit unit = _mapper.Map<Unit>(createDTO[i]);
                        await _dbUnit.CreateAsync(unit);
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
