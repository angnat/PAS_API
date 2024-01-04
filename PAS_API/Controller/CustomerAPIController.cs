using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PAS_API.Model;
using PAS_API.Model.DTO;
using PAS_API.Repository.IRepository;

namespace PAS_API.Controller
{
    [Route("api/CustomerAPI")]
    [ApiController]
    public class CustomerAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ICustomerRepository _db_Customer;
        private readonly ICustomerAddressRepository _db_CustomerAddress;
        private readonly ICustomerCommunicationRepository _db_CustomerCommunication;
        private readonly IMapper _mapper;
        public CustomerAPIController(ICustomerRepository db_Customer, IMapper mapper, ICustomerAddressRepository db_CustomerAddress, ICustomerCommunicationRepository db_CustomerCommunication)
        {
            _db_Customer = db_Customer;           
            _db_CustomerAddress = db_CustomerAddress;
            _db_CustomerCommunication = db_CustomerCommunication;
            _mapper = mapper;
            this._response = new();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> InsertZMCustomer([FromBody] CreateCustomerDTO[] createDTO)
        {
            try
            {
                for (int i = 0; i < createDTO.Length; i++)
                {
                    var exsistingCustomer = await _db_Customer.GetAsync(u => u.CustomerID.ToLower() == createDTO[i].CustomerID.ToLower());
                    if (exsistingCustomer != null)
                    {
                        // Update Customer                           
                        _mapper.Map(createDTO[i], exsistingCustomer); // Update the existing progress with the new values
                        

                        //var existingProject = await _dbProject.GetAsync(u => u.ProjectCode.ToLower() == exsistingCustomer.BACode);

                        //if (existingProject == null)
                        //{
                        //    // Create a new project
                        //    var newProject = new Project
                        //    {
                        //        FIDDivision = 530,
                        //        ProjectCode = exsistingCustomer.BACode,
                        //        ProjectName = exsistingCustomer.BADesc
                        //    };
                        //    await _dbProject.CreateAsync(newProject);

                        //    var newCluster = new Cluster
                        //    {
                        //        FIDProject = newProject.ID,
                        //        SLoc = exsistingCustomer.SLoc,
                        //        ClusterName = exsistingCustomer.SLocDescription
                        //    };

                        //    await _dbCluster.CreateAsync(newCluster);
                        //}
                        //else
                        //{
                        //    long? projID = existingProject.ID;
                        //    //projectnya ada maka dia akan insert clusternya , cek dulu Clusternya   

                        //    var existingCluster = await _dbCluster.GetAsync(u => u.FIDProject == projID);
                        //    var newCluster = new Cluster
                        //    {
                        //        FIDProject = projID,
                        //        SLoc = exsistingCustomer.SLoc,
                        //        ClusterName = exsistingCustomer.SLocDescription
                        //    };

                        //    await _dbCluster.CreateAsync(newCluster);
                        //    exsistingCustomer.FIDCluster = newCluster.ID;// Update the FIDCluster value                            
                        //}

                        await _db_Customer.UpdateAsync(exsistingCustomer);
                    }
                    else
                    {
                        if (createDTO == null) return BadRequest(createDTO[i]);
                        Customer customer = _mapper.Map<Customer>(createDTO[i]);
                        if (createDTO[i].FirstName.Substring(0, 2) == "PT" || createDTO[i].FirstName.Substring(0, 2) == "CV")
                        {
                            customer.Title = "Company";
                        }
                        await _db_Customer.CreateAsync(customer);

                        if (!string.IsNullOrEmpty(createDTO[i].AlamatSurat))
                        {                          
                            List<CustomerAddress> customerAddresses = new List<CustomerAddress>();
                            CustomerAddress alamatSurat = new CustomerAddress
                            {
                                FIDAddressType = 2,
                                Street = createDTO[i].AlamatSurat,
                                FIDCustomer = customer.ID
                            };
                            customerAddresses.Add(alamatSurat);
                            CustomerAddress alamatKTP = new CustomerAddress
                            {
                                FIDAddressType = 3,
                                Street = createDTO[i].Alamat,
                                FIDCustomer = customer.ID
                            };
                            customerAddresses.Add(alamatKTP);

                            for (int j = 0; j < customerAddresses.Count; j++)
                            {
                                CustomerAddress customerAddress = _mapper.Map<CustomerAddress>(customerAddresses[j]);
                                await _db_CustomerAddress.CreateAsync(customerAddresses[j]);
                            }
                        }

                        List<CustomerCommunication> lstCustomerCommunication = new List<CustomerCommunication>();
                        CustomerCommunication noHp = new CustomerCommunication
                        {
                            FIDCommunicationType = 1,
                            FIDCommunicationInfo = createDTO[i].CellPhoneNumber,
                            FIDCustomer = customer.ID
                        };
                        lstCustomerCommunication.Add(noHp);
                        CustomerCommunication notelp = new CustomerCommunication
                        {
                            FIDCommunicationType = 2,
                            FIDCommunicationInfo = createDTO[i].PhoneNumber,
                            FIDCustomer = customer.ID
                        };
                        lstCustomerCommunication.Add(notelp);

                        for (int k = 0; k < lstCustomerCommunication.Count; k++)
                        {
                            CustomerCommunication customerCommunication = _mapper.Map<CustomerCommunication>(lstCustomerCommunication[k]);
                            await _db_CustomerCommunication.CreateAsync(lstCustomerCommunication[k]);
                        }
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
