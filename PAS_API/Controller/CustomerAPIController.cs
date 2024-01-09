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
                if (createDTO == null)
                {
                    _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                for (int i = 0; i < createDTO.Length; i++)
                {
                    /* Update Customer */
                    var customerDTO = createDTO[i];                  
                    var exsistingCustomer = await _db_Customer.GetAsync(u => u.CustomerID.ToLower() == createDTO[i].CustomerID.ToLower());
                    if (exsistingCustomer != null)
                    {
                        // Update Customer                           
                        _mapper.Map(customerDTO, exsistingCustomer); // Update the existing customer with the new values                                            
                        await _db_Customer.UpdateAsync(exsistingCustomer);

                        List<CustomerAddress> customerAddresses = new List<CustomerAddress>();
                        var exsistingAlamatKTP = await _db_CustomerAddress.GetAsync(u => u.FIDCustomer == exsistingCustomer.ID && u.FIDAddressType == "3");
                        var exsistingAlamatSurat = await _db_CustomerAddress.GetAsync(u => u.FIDCustomer == exsistingCustomer.ID && u.FIDAddressType == "2");

                        var exsistingNoHP = await _db_CustomerCommunication.GetAsync(u => u.FIDCustomer == exsistingCustomer.ID && u.FIDCommunicationType == 1);
                        var exsistingNoTelp = await _db_CustomerCommunication.GetAsync(u => u.FIDCustomer == exsistingCustomer.ID && u.FIDCommunicationType == 2);

                        CustomerAddress alamatKTP = new CustomerAddress
                        {
                            FIDAddressType = "3",
                            Street = createDTO[i].Alamat,
                            FIDCustomer = exsistingCustomer.ID
                        };
                        customerAddresses.Add(alamatKTP);
                        CustomerAddress alamatSurat = new CustomerAddress
                        {
                            FIDAddressType = "2",
                            Street = createDTO[i].AlamatSurat,
                            FIDCustomer = exsistingCustomer.ID
                        };
                        customerAddresses.Add(alamatSurat);

                        CustomerAddress customerAddress = _mapper.Map<CustomerAddress>(customerAddresses.Find(u => u.FIDAddressType == "3"));
                        if (exsistingAlamatKTP == null)
                        {                            
                            await _db_CustomerAddress.CreateAsync(customerAddresses.Find(u => u.FIDAddressType == "3"));
                        }
                        else
                        {
                            await _db_CustomerAddress.UpdateAsync(customerAddresses.Find(u => u.FIDAddressType == "3"));
                        }
                        
                        CustomerAddress customerAddressLetter = _mapper.Map<CustomerAddress>(customerAddresses.Find(u => u.FIDAddressType == "2"));
                        if (exsistingAlamatSurat == null)
                        {
                            await _db_CustomerAddress.CreateAsync(customerAddresses.Find(u => u.FIDAddressType == "2"));
                        }
                        else
                        {
                            await _db_CustomerAddress.UpdateAsync(customerAddresses.Find(u => u.FIDAddressType == "2"));
                        }

                        List<CustomerCommunication> lstCustomerCommunication = new List<CustomerCommunication>();
                        CustomerCommunication noHp = new CustomerCommunication
                        {
                            FIDCommunicationType = 1,
                            FIDCommunicationInfo = createDTO[i].CellPhoneNumber,
                            FIDCustomer = exsistingCustomer.ID
                        };
                        lstCustomerCommunication.Add(noHp);
                        CustomerCommunication notelp = new CustomerCommunication
                        {
                            FIDCommunicationType = 2,
                            FIDCommunicationInfo = createDTO[i].PhoneNumber,
                            FIDCustomer = exsistingCustomer.ID
                        };
                        lstCustomerCommunication.Add(notelp);

                        if(exsistingNoHP ==  null)
                        {
                            await _db_CustomerCommunication.CreateAsync(lstCustomerCommunication.Find(u => u.FIDCommunicationType == 1));
                        }
                        else
                        {
                            await _db_CustomerCommunication.UpdateAsync(lstCustomerCommunication.Find(u => u.FIDCommunicationType == 1));
                        }

                        if(exsistingNoTelp ==  null)
                        {
                            await _db_CustomerCommunication.CreateAsync(lstCustomerCommunication.Find(u => u.FIDCommunicationType == 2));
                        }
                        else
                        {
                            await _db_CustomerCommunication.UpdateAsync(lstCustomerCommunication.Find(u => u.FIDCommunicationType == 2));
                        }

                        _response.StatusCode = System.Net.HttpStatusCode.Created;
                        _response.IsSuccess = true;

                        return Ok(_response);
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
                                FIDAddressType = "2",
                                Street = createDTO[i].AlamatSurat,
                                FIDCustomer = customer.ID
                            };
                            customerAddresses.Add(alamatSurat);
                            CustomerAddress alamatKTP = new CustomerAddress
                            {
                                FIDAddressType = "3",
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

        private string SplitAlamat(string Alamat)
        {
            // string alamat = "PERUM CITRA NIRWANA NO.A-14 RT.004 RW.059 SINDUADI, MLATI KABUPATEN SLEMAN DAERAH ISTIMEWA YOGYAKARTA dan DE LATINOS CLUSTER CENTRO HAVANA BLOK M.11/0";
            string addressLine1 = "";
            string addressLine2 = "";

            string street = Alamat.Substring(0, Alamat.Length >= 60 ? 60 : Alamat.Length);
            if (Alamat.Length > 60 && !Alamat.Substring(60).StartsWith(" "))
                street = street.Substring(0, street.LastIndexOf(" ") + 1);

            int tot1 = street.Length;
            if (Alamat.Length >= tot1 + 40)
            {
                addressLine1 = Alamat.Substring(tot1, 40);
                if (Alamat.Length > tot1 + 40 && !Alamat.Substring(tot1 + 40).StartsWith(" "))
                    addressLine1 = addressLine1.Substring(0, addressLine1.LastIndexOf(" ") + 1);

                int tot2 = street.Length + addressLine1.Length;
                if (Alamat.Length >= tot2 + 40)
                {
                    addressLine2 = Alamat.Substring(tot2, 40);
                    if (Alamat.Length > tot2 + 40 && !Alamat.Substring(tot2 + 40).StartsWith(" "))
                        addressLine2 = addressLine2.Substring(0, addressLine2.LastIndexOf(" ") + 1);
                }
                else
                    addressLine2 = Alamat.Substring(tot2);
            }
            else
                addressLine1 = Alamat.Substring(tot1);

            street = street.Trim();
            addressLine1 = addressLine1.Trim();
            addressLine2 = addressLine2.Trim();

            return street + ";" + addressLine1 + ";" + addressLine2;
        }
    }
}
