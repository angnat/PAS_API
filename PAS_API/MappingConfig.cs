using AutoMapper;
using PAS_API.Model;
using PAS_API.Model.DTO;

namespace PAS_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Unit, UnitDTO>();
            CreateMap<UnitDTO, Unit>();

            CreateMap<TUnit, TUnitDTO>().ReverseMap();

            CreateMap<AdminUnitTeknik, AdminUnitTeknikDTO>().ReverseMap();
            CreateMap<Progress, ProgressDTO>().ReverseMap();
            CreateMap<Progress, CreateProgressDTO>().ReverseMap();
            CreateMap<Unit, CreateUnitDTO>().ReverseMap();
            CreateMap<Cluster, ClusterDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Customer, CreateCustomerDTO>().ReverseMap();
            CreateMap<CustomerAddress, CreateCustomerAddressDTO>().ReverseMap();
            CreateMap<CustomerCommunication, CreateCustomerCommunicationDTO>().ReverseMap();
        }
    }
}
