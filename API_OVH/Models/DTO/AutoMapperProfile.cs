using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using AutoMapper;

namespace API_OVH
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // TypeEquipement
            CreateMap<TypeEquipement, TypeEquipementDTO>()
                .ForMember(dest => dest.NomTypeEquipement, opt => opt.MapFrom(src => src.NomTypeEquipement))
                .ForMember(dest => dest.IdTypeEquipement, opt => opt.MapFrom(src => src.IdTypeEquipement))
                .ReverseMap()
                .ForMember(dest => dest.NomTypeEquipement, opt => opt.MapFrom(src => src.NomTypeEquipement))
                .ForMember(dest => dest.Equipements, opt => opt.Ignore());

            // TypeSalle
            CreateMap<TypeSalle, TypeSalleDTO>()
                .ForMember(dest => dest.IdTypeSalle, opt => opt.MapFrom(src => src.IdTypeSalle))
                .ForMember(dest => dest.NomTypeSalle, opt => opt.MapFrom(src => src.NomTypeSalle))
                .ReverseMap()
                .ForMember(dest => dest.Salles, opt => opt.Ignore());

            // Map entre Unite et UniteDTO
            CreateMap<Unite, UniteDTO>()
                .ForMember(dest => dest.IdUnite, opt => opt.MapFrom(src => src.IdUnite))
                .ForMember(dest => dest.NomUnite, opt => opt.MapFrom(src => src.NomUnite))
                .ForMember(dest => dest.SigleUnite, opt => opt.MapFrom(src => src.SigleUnite))
                .ReverseMap()
                .ForMember(dest => dest.UnitesCapteur, opt => opt.Ignore());

            // Map entre Unite et UniteDetailDTO
            CreateMap<Unite, UniteDetailDTO>()
                .ForMember(dest => dest.IdUnite, opt => opt.MapFrom(src => src.IdUnite))
                .ForMember(dest => dest.NomUnite, opt => opt.MapFrom(src => src.NomUnite))
                .ForMember(dest => dest.SigleUnite, opt => opt.MapFrom(src => src.SigleUnite))
                // Mapper la liste des capteurs à partir de la collection UnitesCapteur
                .ForMember(dest => dest.Capteurs, opt => opt.MapFrom(src => src.UnitesCapteur.Select(uc => uc.CapteurNavigation)))
                .ReverseMap()
                .ForMember(dest => dest.UnitesCapteur, opt => opt.Ignore());

        }
    }
}
