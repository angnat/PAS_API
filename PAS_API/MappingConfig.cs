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

            //CreateMap<Villa, VillaCreateDto>().ReverseMap();
            //CreateMap<Villa, VillaUpdateDto>().ReverseMap();
        }
    }
}
