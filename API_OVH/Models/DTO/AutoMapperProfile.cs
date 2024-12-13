﻿using API_OVH.Models.DTO;
using API_OVH.Models.EntityFramework;
using AutoMapper;

namespace API_OVH
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Batiment - BatimentDTO
            CreateMap<Batiment, BatimentDTO>()
                .ForMember(dest => dest.NomBatiment, opt => opt.MapFrom(src => src.NomBatiment))
                .ForMember(dest => dest.IdBatiment, opt => opt.MapFrom(src => src.IdBatiment))
                .ForMember(dest => dest.NbSalle, opt => opt.MapFrom(src => src.Salles.Count))
                .ReverseMap()
                .ForMember(dest => dest.NomBatiment, opt => opt.MapFrom(src => src.NomBatiment))
                .ForMember(dest => dest.IdBatiment, opt => opt.MapFrom(src => src.IdBatiment))
                .ForMember(dest => dest.Salles, opt => opt.Ignore());
            
            // Batiment BatimentSansNavigationDTO
            CreateMap<Batiment, BatimentSansNavigationDTO>()
                .ForMember(dest => dest.NomBatiment, opt => opt.MapFrom(src => src.NomBatiment))
                .ForMember(dest => dest.IdBatiment, opt => opt.MapFrom(src => src.IdBatiment))
                .ReverseMap()
                .ForMember(dest => dest.NomBatiment, opt => opt.MapFrom(src => src.NomBatiment))
                .ForMember(dest => dest.IdBatiment, opt => opt.MapFrom(src => src.IdBatiment));

            CreateMap<Batiment, BatimentDetailDTO>()
                .ForMember(dest => dest.NomBatiment, opt => opt.MapFrom(src => src.NomBatiment))
                .ForMember(dest => dest.IdBatiment, opt => opt.MapFrom(src => src.IdBatiment))
                .ForMember(dest => dest.Salles, opt => opt.MapFrom(src => src.Salles))
                .ReverseMap()
                .ForMember(dest => dest.NomBatiment, opt => opt.MapFrom(src => src.NomBatiment))
                .ForMember(dest => dest.IdBatiment, opt => opt.MapFrom(src => src.IdBatiment))
                .ForMember(dest => dest.Salles, opt => opt.MapFrom(src => src.Salles));

            // Capteur - CapteurDetailDTO
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

            // Capteur - CapteurSansNavigationDTO
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

            // Capteur - CapteurDTO
            CreateMap<Capteur, CapteurDTO>()
                .ForMember(dest => dest.IdCapteur, opt => opt.MapFrom(src => src.IdCapteur))
                .ForMember(dest => dest.NomCapteur, opt => opt.MapFrom(src => src.NomCapteur))
                .ForMember(dest => dest.NomSalle, opt => opt.MapFrom(src => src.MurNavigation.SalleNavigation.NomSalle));

            // Equipement - EquipementDTO
            CreateMap<Equipement, EquipementDTO>()
                .ForMember(dest => dest.IdEquipement, opt => opt.MapFrom(src => src.IdEquipement))
                .ForMember(dest => dest.NomEquipement, opt => opt.MapFrom(src => src.NomEquipement))
                .ForMember(dest => dest.NomSalleEquipement, opt => opt.MapFrom(src => src.MurNavigation.SalleNavigation.NomSalle))
                .ForMember(dest => dest.NomTypeEquipement, opt => opt.MapFrom(src => src.TypeEquipementNavigation.NomTypeEquipement))
                .ForMember(dest => dest.Dimensions, opt => opt.MapFrom(src => "" + src.Longueur + "x" + src.Largeur + "x" + src.Hauteur));

            // Equipement - EquipementDetailDTO
            CreateMap<Equipement, EquipementDetailDTO>()
                .ForMember(dest => dest.IdEquipement, opt => opt.MapFrom(src => src.IdEquipement))
                .ForMember(dest => dest.NomEquipement, opt => opt.MapFrom(src => src.NomEquipement))
                .ForMember(dest => dest.TypeEquipement, opt => opt.MapFrom(src => src.TypeEquipementNavigation))
                .ForMember(dest => dest.Dimensions, opt => opt.MapFrom(src => "" + src.Longueur + "x" + src.Largeur + "x" + src.Hauteur))
                .ForMember(dest => dest.EstActif, opt => opt.MapFrom(src => src.EstActif))
                .ForMember(dest => dest.PositionX, opt => opt.MapFrom(src => src.XEquipement))
                .ForMember(dest => dest.PositionY, opt => opt.MapFrom(src => src.YEquipement))
                .ForMember(dest => dest.PositionZ, opt => opt.MapFrom(src => src.ZEquipement))
                .ForMember(dest => dest.Salle, opt => opt.MapFrom(src => src.MurNavigation.SalleNavigation));

            // Equipement - EquipementSansNavigationDTO
            CreateMap<Equipement, EquipementSansNavigationDTO>()
                .ForMember(dest => dest.IdEquipement, opt => opt.MapFrom(src => src.IdEquipement))
                .ForMember(dest => dest.IdMur, opt => opt.MapFrom(src => src.IdMur))
                .ForMember(dest => dest.IdTypeEquipement, opt => opt.MapFrom(src => src.IdTypeEquipement))
                .ForMember(dest => dest.NomEquipement, opt => opt.MapFrom(src => src.NomEquipement))
                .ForMember(dest => dest.Longueur, opt => opt.MapFrom(src => src.Longueur))
                .ForMember(dest => dest.Largeur, opt => opt.MapFrom(src => src.Largeur))
                .ForMember(dest => dest.Hauteur, opt => opt.MapFrom(src => src.Hauteur))
                .ForMember(dest => dest.XEquipement, opt => opt.MapFrom(src => src.XEquipement))
                .ForMember(dest => dest.YEquipement, opt => opt.MapFrom(src => src.YEquipement))
                .ForMember(dest => dest.ZEquipement, opt => opt.MapFrom(src => src.ZEquipement))
                .ForMember(dest => dest.EstActif, opt => opt.MapFrom(src => src.EstActif))
                .ReverseMap()
                .ForMember(dest => dest.IdEquipement, opt => opt.MapFrom(src => src.IdEquipement))
                .ForMember(dest => dest.IdMur, opt => opt.MapFrom(src => src.IdMur))
                .ForMember(dest => dest.IdTypeEquipement, opt => opt.MapFrom(src => src.IdTypeEquipement))
                .ForMember(dest => dest.NomEquipement, opt => opt.MapFrom(src => src.NomEquipement))
                .ForMember(dest => dest.Longueur, opt => opt.MapFrom(src => src.Longueur))
                .ForMember(dest => dest.Largeur, opt => opt.MapFrom(src => src.Largeur))
                .ForMember(dest => dest.Hauteur, opt => opt.MapFrom(src => src.Hauteur))
                .ForMember(dest => dest.XEquipement, opt => opt.MapFrom(src => src.XEquipement))
                .ForMember(dest => dest.YEquipement, opt => opt.MapFrom(src => src.YEquipement))
                .ForMember(dest => dest.ZEquipement, opt => opt.MapFrom(src => src.ZEquipement))
                .ForMember(dest => dest.EstActif, opt => opt.MapFrom(src => src.EstActif))
                .ForMember(dest => dest.TypeEquipementNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.MurNavigation, opt => opt.Ignore());


            // Mur - MurDTO
            CreateMap<Mur, MurDTO>()
                .ForMember(dest => dest.IdMur, opt => opt.MapFrom(src => src.IdMur))
                .ForMember(dest => dest.Orientation, opt => opt.MapFrom(src => src.Orientation))
                .ForMember(dest => dest.NomSalle, opt => opt.MapFrom(src => src.SalleNavigation.NomSalle))
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.DirectionNavigation.LettresDirection));

            // Mur - MurSansNavigationDTO
            CreateMap<Mur, MurSansNavigationDTO>()
                .ForMember(dest => dest.IdMur, opt => opt.MapFrom(src => src.IdMur))
                .ForMember(dest => dest.IdDirection, opt => opt.MapFrom(src => src.IdDirection))
                .ForMember(dest => dest.IdSalle, opt => opt.MapFrom(src => src.IdSalle))
                .ForMember(dest => dest.Longueur, opt => opt.MapFrom(src => src.Longueur))
                .ForMember(dest => dest.Hauteur, opt => opt.MapFrom(src => src.Hauteur))
                .ForMember(dest => dest.Orientation, opt => opt.MapFrom(src => src.Orientation))
                .ReverseMap()
                .ForMember(dest => dest.IdMur, opt => opt.MapFrom(src => src.IdMur))
                .ForMember(dest => dest.IdDirection, opt => opt.MapFrom(src => src.IdDirection))
                .ForMember(dest => dest.IdSalle, opt => opt.MapFrom(src => src.IdSalle))
                .ForMember(dest => dest.Longueur, opt => opt.MapFrom(src => src.Longueur))
                .ForMember(dest => dest.Hauteur, opt => opt.MapFrom(src => src.Hauteur))
                .ForMember(dest => dest.Orientation, opt => opt.MapFrom(src => src.Orientation));

            // Mur - MurDetailDTO
            CreateMap<Mur, MurDetailDTO>()
                .ForMember(dest => dest.IdMur, opt => opt.MapFrom(src => src.IdMur))
                .ForMember(dest => dest.IdDirection, opt => opt.MapFrom(src => src.IdDirection))
                .ForMember(dest => dest.IdSalle, opt => opt.MapFrom(src => src.IdSalle))
                .ForMember(dest => dest.Longueur, opt => opt.MapFrom(src => src.Longueur))
                .ForMember(dest => dest.Hauteur, opt => opt.MapFrom(src => src.Hauteur))
                .ForMember(dest => dest.Orientation, opt => opt.MapFrom(src => src.Orientation))
                .ForMember(dest => dest.SalleNavigation, opt => opt.MapFrom(src => src.SalleNavigation))
                .ForMember(dest => dest.DirectionNavigation, opt => opt.MapFrom(src => src.DirectionNavigation))
                .ForMember(dest => dest.Capteurs, opt => opt.MapFrom(src => src.Capteurs))
                .ForMember(dest => dest.Equipements, opt => opt.MapFrom(src => src.Equipements))
                .ReverseMap()
                .ForMember(dest => dest.IdMur, opt => opt.MapFrom(src => src.IdMur))
                .ForMember(dest => dest.IdDirection, opt => opt.MapFrom(src => src.IdDirection))
                .ForMember(dest => dest.IdSalle, opt => opt.MapFrom(src => src.IdSalle))
                .ForMember(dest => dest.Longueur, opt => opt.MapFrom(src => src.Longueur))
                .ForMember(dest => dest.Hauteur, opt => opt.MapFrom(src => src.Hauteur))
                .ForMember(dest => dest.Orientation, opt => opt.MapFrom(src => src.Orientation))
                .ForMember(dest => dest.SalleNavigation, opt => opt.MapFrom(src => src.SalleNavigation))
                .ForMember(dest => dest.DirectionNavigation, opt => opt.MapFrom(src => src.DirectionNavigation))
                .ForMember(dest => dest.Capteurs, opt => opt.MapFrom(src => src.Capteurs))
                .ForMember(dest => dest.Equipements, opt => opt.MapFrom(src => src.Equipements));

            // Direction - DirectionSansNavigationDTO
            CreateMap<Direction, DirectionSansNavigationDTO>()
                .ForMember(dest => dest.IdDirection, opt => opt.MapFrom(src => src.IdDirection))
                .ForMember(dest => dest.LettresDirection, opt => opt.MapFrom(src => src.LettresDirection))
                .ReverseMap()
                .ForMember(dest => dest.IdDirection, opt => opt.MapFrom(src => src.IdDirection))
                .ForMember(dest => dest.LettresDirection, opt => opt.MapFrom(src => src.LettresDirection));


            // Direction - DirectionDetailDTO
            CreateMap<Direction, DirectionDetailDTO>()
                .ForMember(dest => dest.IdDirection, opt => opt.MapFrom(src => src.IdDirection))
                .ForMember(dest => dest.LettresDirection, opt => opt.MapFrom(src => src.LettresDirection))
                .ForMember(dest => dest.Murs, opt => opt.MapFrom(src => src.Murs))
                .ReverseMap()
                .ForMember(dest => dest.IdDirection, opt => opt.MapFrom(src => src.IdDirection))
                .ForMember(dest => dest.LettresDirection, opt => opt.MapFrom(src => src.LettresDirection))
                .ForMember(dest => dest.Murs, opt => opt.MapFrom(src => src.Murs));

            // TypeEquipement - TypeEquipementDTO
            CreateMap<TypeEquipement, TypeEquipementDTO>()
                .ForMember(dest => dest.NomTypeEquipement, opt => opt.MapFrom(src => src.NomTypeEquipement))
                .ForMember(dest => dest.IdTypeEquipement, opt => opt.MapFrom(src => src.IdTypeEquipement))
                .ReverseMap()
                .ForMember(dest => dest.NomTypeEquipement, opt => opt.MapFrom(src => src.NomTypeEquipement))
                .ForMember(dest => dest.IdTypeEquipement, opt => opt.MapFrom(src => src.IdTypeEquipement))
                .ForMember(dest => dest.Equipements, opt => opt.Ignore());

            // TypeEquipement - TypeEquipementDetailDTO
            CreateMap<TypeEquipement, TypeEquipementDetailDTO>()
                .ForMember(dest => dest.NomTypeEquipement, opt => opt.MapFrom(src => src.NomTypeEquipement))
                .ForMember(dest => dest.IdTypeEquipement, opt => opt.MapFrom(src => src.IdTypeEquipement))
                .ForMember(dest => dest.Equipements, opt => opt.MapFrom(src => src.Equipements))
                .ReverseMap()
                .ForMember(dest => dest.NomTypeEquipement, opt => opt.MapFrom(src => src.NomTypeEquipement))
                .ForMember(dest => dest.IdTypeEquipement, opt => opt.MapFrom(src => src.IdTypeEquipement))
                .ForMember(dest => dest.Equipements, opt => opt.MapFrom(src => src.Equipements));


            // TypeSalle - TypeSalleDTO
            CreateMap<TypeSalle, TypeSalleDTO>()
                .ForMember(dest => dest.IdTypeSalle, opt => opt.MapFrom(src => src.IdTypeSalle))
                .ForMember(dest => dest.NomTypeSalle, opt => opt.MapFrom(src => src.NomTypeSalle))
                .ReverseMap()
                .ForMember(dest => dest.IdTypeSalle, opt => opt.MapFrom(src => src.IdTypeSalle))
                .ForMember(dest => dest.NomTypeSalle, opt => opt.MapFrom(src => src.NomTypeSalle))
                .ForMember(dest => dest.Salles, opt => opt.Ignore());

            // TypeSalle - TypeSalleDetailDTO
            CreateMap<TypeSalle, TypeSalleDetailDTO>()
                .ForMember(dest => dest.IdTypeSalle, opt => opt.MapFrom(src => src.IdTypeSalle))
                .ForMember(dest => dest.NomTypeSalle, opt => opt.MapFrom(src => src.NomTypeSalle))
                .ForMember(dest => dest.Salles, opt => opt.MapFrom(src => src.Salles))
                .ReverseMap()
                .ForMember(dest => dest.IdTypeSalle, opt => opt.MapFrom(src => src.IdTypeSalle))
                .ForMember(dest => dest.NomTypeSalle, opt => opt.MapFrom(src => src.NomTypeSalle))
                .ForMember(dest => dest.Salles, opt => opt.MapFrom(src => src.Salles));

            // Unite - UniteDTO
            CreateMap<Unite, UniteDTO>()
                .ForMember(dest => dest.IdUnite, opt => opt.MapFrom(src => src.IdUnite))
                .ForMember(dest => dest.NomUnite, opt => opt.MapFrom(src => src.NomUnite))
                .ForMember(dest => dest.SigleUnite, opt => opt.MapFrom(src => src.SigleUnite))
                .ReverseMap()
                .ForMember(dest => dest.IdUnite, opt => opt.MapFrom(src => src.IdUnite))
                .ForMember(dest => dest.NomUnite, opt => opt.MapFrom(src => src.NomUnite))
                .ForMember(dest => dest.SigleUnite, opt => opt.MapFrom(src => src.SigleUnite))
                .ForMember(dest => dest.UnitesCapteur, opt => opt.Ignore());

            // Unite - UniteDetailDTO
            CreateMap<Unite, UniteDetailDTO>()
                .ForMember(dest => dest.IdUnite, opt => opt.MapFrom(src => src.IdUnite))
                .ForMember(dest => dest.NomUnite, opt => opt.MapFrom(src => src.NomUnite))
                .ForMember(dest => dest.SigleUnite, opt => opt.MapFrom(src => src.SigleUnite))
                // Mapper la liste des capteurs à partir de la collection UnitesCapteur
                .ForMember(dest => dest.Capteurs, opt => opt.MapFrom(src => src.UnitesCapteur.Select(uc => uc.CapteurNavigation)))
                .ReverseMap()
                .ForMember(dest => dest.IdUnite, opt => opt.MapFrom(src => src.IdUnite))
                .ForMember(dest => dest.NomUnite, opt => opt.MapFrom(src => src.NomUnite))
                .ForMember(dest => dest.SigleUnite, opt => opt.MapFrom(src => src.SigleUnite))
                .ForMember(dest => dest.UnitesCapteur, opt => opt.Ignore());

            // UniteCapteur - UniteCapteurSansNavigationDTO
            CreateMap<UniteCapteur, UniteCapteurSansNavigationDTO>()
                .ForMember(dest => dest.IdCapteur, opt => opt.MapFrom(src => src.IdCapteur))
                .ForMember(dest => dest.IdUnite, opt => opt.MapFrom(src => src.IdUnite))
                .ReverseMap()
                .ForMember(dest => dest.IdCapteur, opt => opt.MapFrom(src => src.IdCapteur))
                .ForMember(dest => dest.IdUnite, opt => opt.MapFrom(src => src.IdUnite))
                .ForMember(dest => dest.CapteurNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.UniteNavigation, opt => opt.Ignore());

            // UniteCapteur - UniteCapteurSansNavigationDTO
            CreateMap<UniteCapteur, UniteCapteurDetailDTO>()
                .ForMember(dest => dest.Capteur, opt => opt.MapFrom(src => new Capteur{
                    IdCapteur = src.CapteurNavigation.IdCapteur,
                    NomCapteur = src.CapteurNavigation.NomCapteur,
                    EstActif = src.CapteurNavigation.EstActif,
                    IdMur = src.CapteurNavigation.IdMur,
                    XCapteur = src.CapteurNavigation.XCapteur,
                    YCapteur = src.CapteurNavigation.YCapteur,
                    ZCapteur = src.CapteurNavigation.ZCapteur
                }
                ))
                .ForMember(dest => dest.Unite, opt => opt.MapFrom(src => new Unite
                {
                    IdUnite = src.UniteNavigation.IdUnite,
                    NomUnite = src.UniteNavigation.NomUnite,
                    SigleUnite = src.UniteNavigation.SigleUnite
                }
                 ))
                .ReverseMap()
                .ForMember(dest => dest.IdCapteur, opt => opt.MapFrom(src => src.Unite.IdUnite))
                .ForMember(dest => dest.IdUnite, opt => opt.MapFrom(src => src.Capteur.IdCapteur))
                .ForMember(dest => dest.CapteurNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.UniteNavigation, opt => opt.Ignore());

            // Mapping pour Salle vers SalleDTO
            CreateMap<Salle, SalleDTO>()
                .ForMember(dest => dest.IdSalle, opt => opt.MapFrom(src => src.IdSalle))
                .ForMember(dest => dest.NomSalle, opt => opt.MapFrom(src => src.NomSalle))
                .ForMember(dest => dest.NomBatiment, opt => opt.MapFrom(src => src.BatimentNavigation != null ? src.BatimentNavigation.NomBatiment : string.Empty))
                .ForMember(dest => dest.NomType, opt => opt.MapFrom(src => src.TypeSalleNavigation != null ? src.TypeSalleNavigation.NomTypeSalle : string.Empty))
                .ReverseMap()
                .ForMember(dest => dest.BatimentNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.TypeSalleNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Murs, opt => opt.Ignore());

            // Mapping pour Salle vers SalleSansNavigation
            CreateMap<Salle, SalleSansNavigationDTO>()
                .ForMember(dest => dest.IdSalle, opt => opt.MapFrom(src => src.IdSalle))
                .ForMember(dest => dest.NomSalle, opt => opt.MapFrom(src => src.NomSalle))
                .ForMember(dest => dest.IdBatiment, opt => opt.MapFrom(src => src.IdBatiment))
                .ForMember(dest => dest.IdTypeSalle, opt => opt.MapFrom(src => src.IdTypeSalle))
                .ReverseMap();

            // Mapping pour Salle vers SalleDetailDTO
            CreateMap<Salle, SalleDetailDTO>()
                .ForMember(dest => dest.IdSalle, opt => opt.MapFrom(src => src.IdSalle))
                .ForMember(dest => dest.NomSalle, opt => opt.MapFrom(src => src.NomSalle))

                .ForMember(dest => dest.Batiment, opt => opt.MapFrom(src => src.BatimentNavigation))
                .ForMember(dest => dest.TypeSalle, opt => opt.MapFrom(src => src.TypeSalleNavigation))

                // Mapper les équipements et capteurs des murs
                .ForMember(dest => dest.Capteurs, opt => opt.MapFrom(src => src.Murs.SelectMany(mur => mur.Capteurs)))
                .ForMember(dest => dest.Equipements, opt => opt.MapFrom(src => src.Murs.SelectMany(mur => mur.Equipements)))

                // Optionnel : Mapper les murs si nécessaire
                .ForMember(dest => dest.Murs, opt => opt.MapFrom(src => src.Murs))

                // Mappage inverse : ignorer certains membres dans l'autre sens si nécessaire
                .ReverseMap()
                .ForMember(dest => dest.BatimentNavigation, opt => opt.MapFrom(src => src.Batiment))
                .ForMember(dest => dest.TypeSalleNavigation, opt => opt.MapFrom(src => src.TypeSalle));
        }
    }
}
