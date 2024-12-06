using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using AutoMapper;

namespace API_OVH
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Batiment
            CreateMap<Batiment, BatimentDTO>()
                .ForMember(dest => dest.NomBatiment, opt => opt.MapFrom(src => src.NomBatiment))
                .ForMember(dest => dest.IdBatiment, opt => opt.MapFrom(src => src.IdBatiment))
                .ForMember(dest => dest.NbSalle, opt => opt.MapFrom(src => src.Salles.Count))
                .ReverseMap()
                .ForMember(dest => dest.NomBatiment, opt => opt.MapFrom(src => src.NomBatiment))
                .ForMember(dest => dest.IdBatiment, opt => opt.MapFrom(src => src.IdBatiment))
                .ForMember(dest => dest.Salles, opt => opt.Ignore());

            CreateMap<Batiment, BatimentSansNavigationDTO>()
                .ForMember(dest => dest.NomBatiment, opt => opt.MapFrom(src => src.NomBatiment))
                .ForMember(dest => dest.IdBatiment, opt => opt.MapFrom(src => src.IdBatiment))
                .ReverseMap()
                .ForMember(dest => dest.NomBatiment, opt => opt.MapFrom(src => src.NomBatiment))
                .ForMember(dest => dest.IdBatiment, opt => opt.MapFrom(src => src.IdBatiment));

            // Capteur
            CreateMap<Capteur, CapteurDetailDTO>()
                .ForMember(dest => dest.IdCapteur, opt => opt.MapFrom(src => src.IdCapteur))
                .ForMember(dest => dest.NomCapteur, opt => opt.MapFrom(src => src.NomCapteur))
                .ForMember(dest => dest.EstActif, opt => opt.MapFrom(src => src.EstActif))
                .ForMember(dest => dest.XCapteur, opt => opt.MapFrom(src => src.XCapteur))
                .ForMember(dest => dest.YCapteur, opt => opt.MapFrom(src => src.YCapteur))
                .ForMember(dest => dest.ZCapteur, opt => opt.MapFrom(src => src.ZCapteur))
                .ForMember(dest => dest.Mur, opt => opt.MapFrom(src => src.MurNavigation))
                .ForMember(dest => dest.Salle, opt => opt.MapFrom(src => src.MurNavigation.SalleNavigation))
                .ForMember(dest => dest.Unites, opt => opt.MapFrom(src => src.UnitesCapteur.Select(uc => uc.UniteNavigation)))
                .ReverseMap()
                .ForMember(dest => dest.IdCapteur, opt => opt.MapFrom(src => src.IdCapteur))
                .ForMember(dest => dest.NomCapteur, opt => opt.MapFrom(src => src.NomCapteur))
                .ForMember(dest => dest.EstActif, opt => opt.MapFrom(src => src.EstActif))
                .ForMember(dest => dest.XCapteur, opt => opt.MapFrom(src => src.XCapteur))
                .ForMember(dest => dest.YCapteur, opt => opt.MapFrom(src => src.YCapteur))
                .ForMember(dest => dest.ZCapteur, opt => opt.MapFrom(src => src.ZCapteur))
                .ForMember(dest => dest.MurNavigation, opt => opt.MapFrom(src => src.Mur))
                .ForMember(dest => dest.UnitesCapteur, opt => opt.MapFrom(src => src.Unites.Select(unite => new UniteCapteur // Pour chaque Unite on "recrée" l'UniteCapteur correspondant en prenant l'id de l'Unite avec l'id du Capteur
                {
                    IdUnite = unite.IdUnite,
                    IdCapteur = src.IdCapteur
                })));

            CreateMap<Capteur, CapteurSansNavigationDTO>()
                .ForMember(dest => dest.IdCapteur, opt => opt.MapFrom(src => src.IdCapteur))
                .ForMember(dest => dest.IdMur, opt => opt.MapFrom(src => src.IdMur))
                .ForMember(dest => dest.NomCapteur, opt => opt.MapFrom(src => src.NomCapteur))
                .ForMember(dest => dest.EstActif, opt => opt.MapFrom(src => src.EstActif))
                .ForMember(dest => dest.XCapteur, opt => opt.MapFrom(src => src.XCapteur))
                .ForMember(dest => dest.YCapteur, opt => opt.MapFrom(src => src.YCapteur))
                .ForMember(dest => dest.ZCapteur, opt => opt.MapFrom(src => src.ZCapteur))
                .ReverseMap()
                .ForMember(dest => dest.IdCapteur, opt => opt.MapFrom(src => src.IdCapteur))
                .ForMember(dest => dest.IdMur, opt => opt.MapFrom(src => src.IdMur))
                .ForMember(dest => dest.NomCapteur, opt => opt.MapFrom(src => src.NomCapteur))
                .ForMember(dest => dest.EstActif, opt => opt.MapFrom(src => src.EstActif))
                .ForMember(dest => dest.XCapteur, opt => opt.MapFrom(src => src.XCapteur))
                .ForMember(dest => dest.YCapteur, opt => opt.MapFrom(src => src.YCapteur))
                .ForMember(dest => dest.ZCapteur, opt => opt.MapFrom(src => src.ZCapteur))
                .ForMember(dest => dest.MurNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.UnitesCapteur, opt => opt.Ignore());

            CreateMap<Capteur, CapteurDTO>()
                .ForMember(dest => dest.IdCapteur, opt => opt.MapFrom(src => src.IdCapteur))
                .ForMember(dest => dest.NomCapteur, opt => opt.MapFrom(src => src.NomCapteur))
                .ForMember(dest => dest.NomSalle, opt => opt.MapFrom(src => src.MurNavigation.SalleNavigation.NomSalle));

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
        }
    }
}
