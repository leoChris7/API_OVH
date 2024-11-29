using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using AutoMapper;

namespace API_OVH
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mappage entre TypeEquipement et TypeEquipementDTO
            CreateMap<TypeEquipement, TypeEquipementDTO>()
                .ForMember(dest => dest.NomTypeEquipement, opt => opt.MapFrom(src => src.NomTypeEquipement))
                .ReverseMap()
                .ForMember(dest => dest.NomTypeEquipement, opt => opt.MapFrom(src => src.NomTypeEquipement))
                .ForMember(dest => dest.Equipements, opt => opt.Ignore());

            // Mappage entre TypeEquipement et TypeEquipementDetailDTO
            CreateMap<TypeEquipement, TypeEquipementDetailDTO>()
                .ForMember(dest => dest.NomTypeEquipement, opt => opt.MapFrom(src => src.NomTypeEquipement))
                .ForMember(dest => dest.Equipements, opt => opt.MapFrom(src => src.Equipements))
                .ReverseMap();



        }
    }
}
